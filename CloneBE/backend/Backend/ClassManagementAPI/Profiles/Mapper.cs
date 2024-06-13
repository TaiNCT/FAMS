using AutoMapper;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Dto.SyllabusDTO;
using ClassManagementAPI.Dto.UserDTO;
using Entities.Models;
using System.Buffers.Text;
using System.Text;

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
            CreateMap<PagedResult<Class>, PagedResult<WeekResultDto>>().ReverseMap();
            CreateMap<Syllabus, GetSyllabusDTO>().ReverseMap();
            CreateMap<Syllabus, SyllabusStatusDTO>().ReverseMap();
            CreateMap<PagedResult<Syllabus>, PagedResult<GetSyllabusDTO>>().ReverseMap();
            CreateMap<ClassUser, InsertResultDTO>().ReverseMap();
            //CreateMap<SyllabusStatusDTO, GetSyllabusWithStatusDTO>().ReverseMap();
            //CreateMap<PagedResult<SyllabusStatusDTO>, PagedResult<GetSyllabusWithStatusDTO>>().ReverseMap();
            //CreateMap<PagedResult<WeekResultDto> , >
            //2 chiều , cần send ngược lại thì xài
            CreateMap<TrainingProgramSyllabus, InsertProgramSyllabusDTO>().ReverseMap();
            CreateMap<Syllabus, InsertSyllabus>().ReverseMap();
            CreateMap<Class, DuplicateClassRequest>().ReverseMap();
        }
    }
}
