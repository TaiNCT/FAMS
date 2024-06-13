using AutoMapper;
using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using StudentInfoManagementAPI.DTO;

namespace StudentInfoManagementAPI.Service
{
    public class AddStudentService : IAddStudentClassService
    {
        private readonly FamsContext _famsContext;
        private readonly IElasticClient _elasticClient;
        private IMapper _mapper;


        public AddStudentService(FamsContext famsContext, IElasticClient elasticClient, IMapper mapper)
        {
            _famsContext = famsContext;
            _elasticClient = elasticClient;
            _mapper = mapper;

        }

     
        public async Task<Student> Add(AddStudentDTO dto)
        {

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var student = new Student
            {
                MutatableStudentId = dto.MutatableStudentId,
                FullName = dto.FullName,
                Dob = dto.Dob,
                Gender = dto.Gender,
                Phone = dto.Phone,
                Email = dto.Email,
                University = dto.University,
                MajorId = dto.MajorId,
                Gpa = dto.Gpa,
                Address = dto.Address,
                GraduatedDate = dto.GraduatedDate,
                Recer = dto.Recer,
                Area = dto.Area,
                Audit = dto.Audit,
                Mock = dto.Mock,
                Faaccount = dto.Faaccount,
                Status = dto.Status,
                JoinedDate = dto.JoinedDate,
            };
            _famsContext.Students.Add(student);
            await _famsContext.SaveChangesAsync();
            var major = _famsContext.Majors.FirstOrDefault(m => m.MajorId == student.MajorId);

            var studentClass = new StudentClass
            {

                AttendingStatus = dto.addStudentClassDTOs.First().AttendingStatus,
                CertificationDate = dto.addStudentClassDTOs.First().CertificationDate,
                CertificationStatus = dto.addStudentClassDTOs.First().CertificationStatus,
                ClassId = dto.addStudentClassDTOs.First().ClassId,
                FinalScore = dto.addStudentClassDTOs.First().FinalScore,
                Gpalevel = dto.addStudentClassDTOs.First().Gpalevel,
                Method = dto.addStudentClassDTOs.First().Method,
                Result = dto.addStudentClassDTOs.First().Result,
                StudentId = student.StudentId

            };
            _famsContext.StudentClasses.Add(studentClass);

            await _famsContext.SaveChangesAsync();
            var studentDTOs = new StudentDTO
            {
                StudentInfoDTO = _mapper.Map<StudentInfoDTO>(student),
                MajorDTO = _mapper.Map<MajorDTO>(student.Major),
                StudentClassDTOs = _mapper.Map<IEnumerable<StudentClassDTO>>(student.StudentClasses.ToList())
            };
            var response = await _elasticClient.IndexDocumentAsync(studentDTOs);
            return student;

        }
    }
}