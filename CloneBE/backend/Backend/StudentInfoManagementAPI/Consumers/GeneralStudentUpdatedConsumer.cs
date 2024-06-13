using AutoMapper;
using Contracts.StudentManagement;
using Entities.Context;
using Entities.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nest;
using Newtonsoft.Json;
using StudentInfoManagementAPI.DTO;

namespace StudentInfoManagementAPI.Consumers
{
    public class GeneralStudentUpdatedCosumer : IConsumer<GeneralStudentUpdated>
    {
        private readonly IMapper _mapper;
        private readonly FamsContext _dbContext;
        private readonly IElasticClient _elasticClient;


        public GeneralStudentUpdatedCosumer(IMapper mapper,
            FamsContext dbContext, IElasticClient elasticClient)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<GeneralStudentUpdated> context)
        {


            Console.WriteLine("--> Consuming General Student's Infor Updated with StudentId: {0}", context.Message.studentid);
            var data = context.Message;
            var student = await _dbContext.Students.Where(sc => sc.StudentId == data.studentid).FirstOrDefaultAsync();

            // Updating the record
            student.Gender = data.gender.Trim();
            student.FullName = data.name.Trim();
            student.Dob = data.dob;

            student.Phone = data.phone.Trim();
            student.Email = data.email.Trim();
            student.Address = data.permanentResidence;
            student.MutatableStudentId = data.sid;



            // This will appear in the StudentClass table
            StudentClass sc = await _dbContext.StudentClasses.Where(sc => sc.StudentId == data.studentid && sc.ClassId == data.classid).FirstOrDefaultAsync();

            sc.AttendingStatus = data.status;
            sc.CertificationStatus = (bool)data.certificateStatus ? 1 : 0;
            sc.CertificationDate = data.certificateDate;

            // Save back the changes
            await _dbContext.SaveChangesAsync();

            //================= Update to elastic ==================
            //Get Document by student id
            var searchResponse = _elasticClient.Search<StudentDTO>(s => s
             .Query(q => q
                 .MatchPhrase(t => t
                     .Field(f => f.StudentInfoDTO.StudentId)
                     .Query(data.studentid)
                 )
             )
         );

            if (searchResponse.Hits.Count == 0)
            {
                Console.WriteLine("No Student Matched");
            }
            else
            {
                //Get new Student status 
                var newStatus = _dbContext.StudentClasses.Any(sc => sc.StudentId == data.studentid && sc.AttendingStatus == "InClass") ? "Active" : "InActive";
                
                
                //Update student infor DTO in elastic
                var studentDocument = searchResponse.Hits.First().Source;
                studentDocument.StudentInfoDTO.FullName = data.name.Trim();
                studentDocument.StudentInfoDTO.Gender = data.gender.Trim();
                studentDocument.StudentInfoDTO.Dob = data.dob;
                studentDocument.StudentInfoDTO.Status = studentDocument.StudentInfoDTO.Status == "Disabled" ? studentDocument.StudentInfoDTO.Status : newStatus;

                studentDocument.StudentInfoDTO.Phone = data.phone.Trim();
                studentDocument.StudentInfoDTO.Email = data.email.Trim();
                studentDocument.StudentInfoDTO.Address = data.permanentResidence.Trim();

                var studentClass = studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == data.classid);
                //Update student in class
                if (studentClass != null)
                {
                    studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == data.classid).AttendingStatus = data.status.Trim();
                    if (data.certificateStatus != null)
                    {
                        studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == data.classid).CertificationStatus = (bool)data.certificateStatus ? 1 : 0;
                    }
                    studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == data.classid).CertificationDate = data.certificateDate;
                }
                else
                {
                    Console.WriteLine("No Student Class Matched");
                }

                var updateResponse = await _elasticClient.UpdateAsync<StudentDTO>(searchResponse.Hits.First().Id, u => u
                    .Doc(studentDocument)
                    .RetryOnConflict(3)
                );

                if (updateResponse.IsValid)
                {
                    Console.WriteLine("Update successful");

                }
                else
                {
                    Console.WriteLine("Update unsuccessful");

                }
            }



        }
    }

}
