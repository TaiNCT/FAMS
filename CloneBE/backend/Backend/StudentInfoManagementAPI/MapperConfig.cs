using AutoMapper;
using Entities.Models;
using StudentInfoManagementAPI.DTO;
namespace OJTStudentManagement
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<StudentInfoDTO, Student>().ReverseMap();
                config.CreateMap<ClassDTO, Class>().ReverseMap();
                config.CreateMap<StudentClassDTO, StudentClass>().ReverseMap();
                config.CreateMap<MajorDTO, Major>().ReverseMap();
                config.CreateMap<Student, StudentClassDTO>().ReverseMap();

            });
            return mapperConfig;

        }
    }
}
