using AutoMapper;
using ReservationManagementAPI.Entities.DTOs;
using Entities.Models;

namespace ReservationManagementAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Module, ModuleDTO>().ReverseMap();
            CreateMap<Class, ClassDTO>().ReverseMap();
            CreateMap<Student, ClassDTO>().ReverseMap();
            CreateMap<ReservedClass, ReservedClassDTO>().ReverseMap();
            CreateMap<QuizStudent, QuizStudentDTO>().ReverseMap();
        }

    }
}
