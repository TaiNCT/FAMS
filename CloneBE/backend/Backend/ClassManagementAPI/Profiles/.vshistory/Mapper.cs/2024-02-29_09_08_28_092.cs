using AutoMapper;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Models;

namespace ClassManagementAPI.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<ClassCreateDto, Class>();
            CreateMap<Class, ClassCreateDto>();

            CreateMap<Class, GetClassDto>().ReverseMap();
            CreateMap<PagedResult<Class>, PagedResult<GetClassDto>>().ReverseMap();
            //CreateMap<PagedResult<WeekResultDto> , >
            //2 chiều , cần send ngược lại thì xài
        }
    }
}
