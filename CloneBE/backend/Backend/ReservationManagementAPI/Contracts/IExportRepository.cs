using ReservationManagementAPI.Entities.DTOs;
using System.Data;

namespace ReservationManagementAPI.Contracts
{
    public interface IExportRepository : IRepositoryBase<DataTable>
    {
        public Task<DataTable> exportReservedStudent(List<StudentReservedDTO> studentReservedList);
    }
}
