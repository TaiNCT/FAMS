using Elasticsearch.Net;
using ReservationManagementAPI.Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationManagementAPI.Contracts
{
    public interface IReservedClassRepository : IRepositoryBase<ReservedClass>
    {

        Task<ReservedClass> InsertReservedClass(string studentId, string classId, string reason, DateTime startDate, DateTime endDate);
        Task<ReservedClassDTO> GetReservedClassByReservedClassId(string reservedClassId);

        //Task<bool> validateInsertReserveStudent(string studentId, string classId, string reason, DateTime startDate, DateTime endDate);
        
    }
}