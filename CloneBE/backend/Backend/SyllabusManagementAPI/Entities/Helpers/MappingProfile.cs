using AutoMapper;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.AssessmentScheme;
using Entities.DTO.SyllabusDay;
using SyllabusManagementAPI.Entities.DTO.SyllabusUnit;
using SyllabusManagementAPI.Entities.DTO.UnitChapter;
using Entities.Context;
using Entities.Models;

namespace SyllabusManagementAPI.Entities.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // AssessmentScheme
            CreateMap<AssessmentSchemeForCreationDTO, AssessmentScheme>().ReverseMap();

            // Syllabus
            CreateMap<Syllabus, SyllabusDTO>().ReverseMap();  
            

            CreateMap<SyllabusForCreationDTO, Syllabus>()
                .ForMember(dest => dest.AssessmentSchemes, opt => opt.MapFrom(src => src.AssessmentSchemes))
                .ForMember(dest => dest.SyllabusDays, opt => opt.MapFrom(src => src.SyllabusDays))
                .ReverseMap();

            CreateMap<SyllabusForUpdateDTO, Syllabus>()
                .ForMember(dest => dest.AssessmentSchemes, opt => opt.MapFrom(src => src.AssessmentSchemes))
                .ForMember(dest => dest.SyllabusDays, opt => opt.MapFrom(src => src.SyllabusDays))
                .ReverseMap();

            // SyllabusDay
            CreateMap<SyllabusDayForCreationDTO, SyllabusDay>()
                .ForMember(dest => dest.SyllabusUnits, opt => opt.MapFrom(src => src.SyllabusUnits))
                .ReverseMap();

            // SyllabusUnit
            CreateMap<SyllabusUnitForCreationDTO, SyllabusUnit>()
                .ForMember(dest => dest.UnitChapters, opt => opt.MapFrom(src => src.UnitChapters))
                .ReverseMap();

            // UnitChapter
            CreateMap<UnitChapterForCreationDTO, UnitChapter>()
                .ForMember(dest => dest.TrainingMaterials, opt => opt.MapFrom(src => src.TrainingMaterials))
                //.ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.DeliveryType))
                //.ForMember(dest => dest.OutputStandard, opt => opt.MapFrom(src => src.OutputStandard))
                .ReverseMap();

			CreateMap<TrainingMaterialForCreationDTO, TrainingMaterial>().ReverseMap();
            CreateMap<DeliveryTypeForCreationDTO, DeliveryType>().ReverseMap();
            CreateMap<OutputStandardForCreationDTO, OutputStandard>().ReverseMap();
            CreateMap<SyllabusDayDTO, SyllabusDay>().ReverseMap();
			CreateMap<SyllabusUnitDTO, SyllabusUnit>().ReverseMap();
			CreateMap<UnitChapterDTO, UnitChapter>().ReverseMap();
			CreateMap<AssessmentSchemeDTO, AssessmentScheme>().ReverseMap();

			CreateMap<SyllabusDayForCreationDTO, AssessmentScheme>().ReverseMap();
            
		}
	}
}
