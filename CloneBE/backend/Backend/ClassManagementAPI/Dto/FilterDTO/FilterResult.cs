using ClassManagementAPI.Dto.ClassDTO;
using Entities.Models;

namespace ClassManagementAPI.Dto.FilterDTO
{
    public class FilterResult
    {
        public PagedResult<Class> ClassResults { get; set; }
        public PagedResult<WeekResultDto> WeekResults { get; set; }
    }
}
