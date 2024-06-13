/*using ClassManagementAPI.Controllers;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
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

                Task<Class> GetClassById(string id);

                Task<PagedResult<WeekResultDto>> GetListOfClassesByDateRange(DateOnly startDate, DateOnly endDate);
                Task<List<Class>> GetClassByAttendeeId(string id);
                Task<List<Class>> GetClassByTime(FilterTime? filterTime);
        }
}
*/