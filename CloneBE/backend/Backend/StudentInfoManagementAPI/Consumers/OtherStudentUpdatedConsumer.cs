using AutoMapper;
using Contracts.StudentManagement;
using Entities.Context;
using Entities.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nest;
using Newtonsoft.Json;
using StudentInfoManagementAPI.DTO;
using System.Collections.Concurrent;

namespace StudentInfoManagementAPI.Consumers
{
    public class OtherStudentUpdatedConsumer : IConsumer<OtherStudentUpdated>
    {

        private readonly IMapper _mapper;
        private readonly FamsContext _dbContext;
        private readonly IElasticClient _elasticClient;


        public OtherStudentUpdatedConsumer(IMapper mapper,
            FamsContext dbContext, IElasticClient elasticClient)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<OtherStudentUpdated> context)
        {

            var data = context.Message;

            //Get student and major want to update
            var student = await _dbContext.Students.Where(sc => sc.StudentId == data.studentid.Trim()).FirstOrDefaultAsync();
            Major major = await _dbContext.Majors.Where(sc => sc.Name == data.major.Trim()).FirstOrDefaultAsync();

            //update student infor
            student.Gpa = data.gpa;
            student.Recer = data.recer;
            student.GraduatedDate = data.gradtime;
            student.University = data.university;

            // Check if the major exists
            if (major != null)
                student.MajorId = major.MajorId;

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
                var studentDocument = searchResponse.Hits.First().Source;
                //update student infor DTO
                studentDocument.StudentInfoDTO.Gpa = data.gpa;
                studentDocument.StudentInfoDTO.Recer = data.recer.Trim();
                studentDocument.StudentInfoDTO.GraduatedDate = data.gradtime;
                studentDocument.StudentInfoDTO.University = data.university.Trim();

                //update student major
                var majorEl = studentDocument.MajorDTO;
                if( majorEl != null && major != null)
                {
                    studentDocument.MajorDTO = _mapper.Map<MajorDTO>(major);
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
