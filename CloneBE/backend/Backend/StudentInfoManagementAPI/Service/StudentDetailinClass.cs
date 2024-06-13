using System.Linq;
using AutoMapper;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using StudentInfoManagementAPI.DTO;

namespace StudentInfoManagementAPI.Service
{
    public class StudentDetailinClass : IStudentDetailinClass
    {
        private readonly FamsContext _dbContext; 
        private readonly IMapper _mapper; 

        public StudentDetailinClass(FamsContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public StudentClassDTO GetStudentDetails(string studentId)
        {
            var studentDetails = _dbContext.StudentClasses
                .Include(sc => sc.Class)
                .Include(sc => sc.Student)
                .SingleOrDefault(sc => sc.StudentId == studentId);

            if (studentDetails == null)
            {
                return null;
            }
            var studentClassDTO = _mapper.Map<StudentClassDTO>(studentDetails);

            return studentClassDTO;
        }
    }
}
