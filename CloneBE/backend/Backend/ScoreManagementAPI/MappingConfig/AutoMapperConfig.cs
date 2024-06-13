
using AutoMapper;
using ScoreManagementAPI.DTO;
using Entities.Models;

namespace ScoreManagementAPI.MappingConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<StudentDTO, Student>().ReverseMap();
        }


    }
}
