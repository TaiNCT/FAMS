using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping classes
            CreateMap<TrainingProgram, TrainingProgramDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<TechnicalCode, TechnicalCodeDto>().ReverseMap();
            CreateMap<TechnicalGroup, TechnicalGroupDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Syllabus, SyllabusDto>().ReverseMap();
            CreateMap<TrainingProgram, CreateTrainingProgramRequest>().ReverseMap();
            CreateMap<TrainingProgram, UpdateTrainingProgramRequest>().ReverseMap();
            CreateMap<TrainingMaterial, TrainingMaterialDto>().ReverseMap();
        }
    }
}