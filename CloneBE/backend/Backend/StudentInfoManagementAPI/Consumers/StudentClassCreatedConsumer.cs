using AutoMapper;
using Contracts.StudentManagement;
using Entities.Context;
using Entities.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using StudentInfoManagementAPI.DTO;
using StudentInfoManagementAPI.Service;

namespace StudentInfoManagementAPI.Consumers
{
    public class StudentClassCreatedConsumer : IConsumer<StudentClassCreated>
    {
        private readonly FamsContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IStudentService_LamNS _service;
        public StudentClassCreatedConsumer(FamsContext dbContext, IMapper mapper, IStudentService_LamNS service)
        {
            IMapper _mapper = mapper;
            _dbcontext = dbContext;
            _service = service; 
        }
        public async Task Consume(ConsumeContext<StudentClassCreated> context)
        {
            var studentClassDTO = context.Message;
            var studentClass = new StudentClass()
            {
                StudentId = studentClassDTO.StudentId,
                ClassId = studentClassDTO.ClassId,
                AttendingStatus = studentClassDTO.AttendingStatus,
                Result = studentClassDTO.Result,
                FinalScore = studentClassDTO.FinalScore,
                Gpalevel = studentClassDTO.Gpalevel,
                CertificationStatus = studentClassDTO.CertificationStatus,
                CertificationDate = studentClassDTO.CertificationDate,
                Method = studentClassDTO.Method
            };
            _dbcontext.StudentClasses.Add(studentClass);
            await _dbcontext.SaveChangesAsync();

            Student nStudent = _dbcontext.Students.FirstOrDefault(s => s.StudentId == studentClassDTO.StudentId);
            await _service.UpdateStudentClassInforElastic(studentClass, nStudent);
        }
    }
}
