using ClassManagementAPI.Controllers;
using ClassManagementAPI.Dto;

using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Dto.FilterDTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;


namespace ClassManagementAPI.Interface
{

    public interface IClassRepository
    {
        Task<List<Class>> GetClasses();

        Task<bool> CreateClass(Class model);

        Task<bool> DeleteClass(int id);

        Task<bool> UpdateClass(Class model);

        Task<int> GetTotalCount();

        Task<PagedResult<Class>> GetClassListByFilter(FilterFormatDto? filterData);
       
        Task<PagedResult<WeekResultDto>> GetClassListInWeekByFilter(FilterFormatDto? filterData);

        Task<Class> GetClassById(string id);

        Task<PagedResult<WeekResultDto>> GetListOfClassesByDateRange(DateOnly startDate, DateOnly endDate);
        Task<List<Class>> GetClassByAttendeeId(string id);
        Task<List<Class>> GetClassByTime(FilterTime? filterTime);

        Task<bool> DuplicatedClass(string Id);
        Task UpdateClassStatus(string classId, string newStatus);
        Task<Class> GetClassById1(string classId);
        Task UpdateClass1(Class updatedClass);
        Task SaveChangesAsync();
        Task AddUserToClassAsync(string userId, string classId);

        Task<List<AttendeeType>> GetALlAttendeeTypeAsync();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
