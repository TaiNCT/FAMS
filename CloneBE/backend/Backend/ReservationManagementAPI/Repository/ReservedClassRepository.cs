using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nest;
using ReservationManagementAPI.Contracts;
using ReservationManagementAPI.Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Context;

namespace ReservationManagementAPI.Repository
{
    public class ReservedClassRepository : RepositoryBase<ReservedClass>, IReservedClassRepository
    {
        private readonly FamsContext _repositoryContext;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public ReservedClassRepository(FamsContext repositoryContext, IMapper mapper, IStudentRepository studentRepository)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }


        

        public async Task<ReservedClass> InsertReservedClass(string studentId,string classId, string reason, DateTime startDate, DateTime endDate)
        {
            
            ReservedClass reservedClass = new ReservedClass
            {
                StudentId = studentId,
                ClassId = classId,
                Reason = reason,
                StartDate = startDate,
                EndDate = endDate,
            };
            RepositoryContext.Add(reservedClass);

            //Change status to reserve
            var studentClass = await RepositoryContext.StudentClasses
                .FirstOrDefaultAsync(st => st.StudentId.ToUpper().Equals(studentId.ToUpper())
                                            && st.ClassId.ToUpper().Equals(classId.ToUpper()));
            if (studentClass != null)
            {
                studentClass.AttendingStatus = "Reserve";
                RepositoryContext.Update(studentClass);
            }
            await _repositoryContext.SaveChangesAsync();

            return reservedClass;
        }

        public async Task<ReservedClassDTO> GetReservedClassByReservedClassId(string reservedClassId)
        {
            var reservedClass = await _repositoryContext.ReservedClasses
                .Where(p => p.ReservedClassId == reservedClassId)
                .FirstOrDefaultAsync();
            ReservedClassDTO reservedClassDTO = new ReservedClassDTO
            {
                StudentId = reservedClass.StudentId,
                ClassId = reservedClass.ClassId,
                Reason = reservedClass.Reason,
                StartDate = reservedClass.StartDate,
                EndDate = reservedClass.EndDate
            };
            return reservedClassDTO;
        }

        //public async Task<bool> validateInsertReserveStudent(string studentId, string classId, string reason, DateTime startDate, DateTime endDate)
        //{
        //    bool status = true;

        //    // validate insert function
        //    var existingReservation = await _repositoryContext.ReservedClasses
        //       .Where(rc => rc.StudentId.ToLower() == studentId.ToLower()).OrderByDescending(rc => rc.EndDate)
        //       .FirstOrDefaultAsync();

        //    if (existingReservation != null && (startDate - existingReservation.EndDate).TotalDays <= 0)
        //    {
        //        throw new Exception("This student has been reserved");
        //        status = false;
        //    }

        //    if (startDate > endDate)
        //    {
        //        throw new Exception("StartDate cannot be greater than EndDate.");
        //        status = false;
        //    }

        //    if ((endDate - startDate).TotalDays > 6 * 30)
        //    {
        //        throw new Exception("The period from StartDate to EndDate cannot exceed 6 months.");
        //        status = false;
        //    }

        //    return status;
        //}
    }
}