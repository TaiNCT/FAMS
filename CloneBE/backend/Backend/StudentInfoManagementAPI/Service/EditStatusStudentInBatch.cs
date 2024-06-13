using AutoMapper;
using Entities.Context;
using StudentInfoManagementAPI.DTO;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace StudentInfoManagementAPI.Service
{
    public class EditStatusStudentInBatch : IEditStatusStudentInBatch
    {
        private readonly FamsContext _context;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;

        public EditStatusStudentInBatch(FamsContext context, IMapper mapper, IElasticClient elasticClient)
        {
            _context = context;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }

        public async Task<ResponseDTO> EditStudentStatusInBatch(string studentIds, string newStatus, string classId)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var mustQueries = new List<Func<QueryContainerDescriptor<StudentDTO>, QueryContainer>>();
                var mustClassQueries = new List<Func<QueryContainerDescriptor<StudentClassDTO>, QueryContainer>>();

                mustQueries?.Add(mq => mq
                    .Match(m => m
                        .Field(f => f.StudentInfoDTO.StudentId)
                        .Query(studentIds)
                    ));

                var searchResponse = await _elasticClient.SearchAsync<StudentDTO>(s => s
                    .From(0)
                    .Size(10000)
                    .Query(q => q
                        .Bool(b => b
                            .Must(mustQueries.Select(q => new Func<QueryContainerDescriptor<StudentDTO>, QueryContainer>(q)).ToArray())
                        )
                    ));


                var studentInElastic = searchResponse.Documents.FirstOrDefault();
                var elasticId = searchResponse.Hits.FirstOrDefault().Id;
                var studentClass = _context.StudentClasses.FirstOrDefault(sc => sc.StudentId == studentIds && sc.ClassId == classId);
                if (studentClass == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Student with ID {studentIds} not found.";
                    return response;
                }

                var classInfo = _context.Classes.FirstOrDefault(c => c.ClassId == studentClass.ClassId);

                if (studentClass.AttendingStatus == newStatus)
                {
                    response.IsSuccess = false;
                    response.Message = $"Student with ID {studentIds} already has the selected status.";
                    return response;
                }

                switch (newStatus)
                {
                    case "InClass":
                        if (classInfo == null || classInfo.EndDate < DateOnly.FromDateTime(DateTime.Now))
                        {
                            response.IsSuccess = false;
                            response.Message = $"Unable to change status for student with ID {studentIds}. The course has ended.";
                            return response;
                        }
                        studentClass.AttendingStatus = newStatus;
                        studentInElastic.StudentClassDTOs.FirstOrDefault(c => c.ClassId == studentClass.ClassId).AttendingStatus = newStatus;
                        break;
                    case "DropOut":
                        if (classInfo != null && classInfo.EndDate < DateOnly.FromDateTime(DateTime.Now))
                        {
                            response.IsSuccess = false;
                            response.Message = $"Unable to change status for student with ID {studentIds}. The course has ended.";
                            return response;
                        }
                        studentInElastic.StudentClassDTOs.FirstOrDefault(c => c.ClassId == studentClass.ClassId).AttendingStatus = newStatus;
                        studentClass.AttendingStatus = newStatus;
                        break;
                    case "Finish":
                        if (classInfo == null || classInfo.EndDate >= DateOnly.FromDateTime(DateTime.Now))
                        {
                            response.IsSuccess = false;
                            response.Message = $"Unable to change status for student with ID {studentIds}. The course has not ended yet.";
                            return response;
                        }
                        // Update attending status
                        studentInElastic.StudentClassDTOs.FirstOrDefault(c => c.ClassId == studentClass.ClassId).AttendingStatus = newStatus;
                        studentClass.AttendingStatus = newStatus;
                        break;
                    default:
                        response.IsSuccess = false;
                        response.Message = $"Invalid new status: {newStatus}.";
                        return response;
                }

                _context.SaveChanges();
                var updateResponse = await _elasticClient.UpdateAsync<StudentDTO>(elasticId, u => u
                        .Doc(studentInElastic)
                        .RetryOnConflict(3));


                response.Message = "Student statuses updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while updating student statuses: {ex.Message}";
            }

            return response;
        }
    }
}
