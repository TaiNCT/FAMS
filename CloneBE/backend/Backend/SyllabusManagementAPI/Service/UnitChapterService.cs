using AutoMapper;
using OfficeOpenXml;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.SyllabusDay;
using SyllabusManagementAPI.Entities.DTO.SyllabusUnit;
using SyllabusManagementAPI.Entities.DTO.UnitChapter;
using SyllabusManagementAPI.Entities.Helpers;
using Entities.Models;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Service
{
	public class UnitChapterService : IUnitChapterService
	{
		private readonly IRepositoryWrapper _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly ResponseHandler _responseHandler;

		public UnitChapterService(IRepositoryWrapper repositoryWrapper, ILoggerManager logger, IMapper mapper, ResponseHandler responseHandler)
		{
			_repository = repositoryWrapper;
			_logger = logger;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}

		public async Task<UnitChapterDTO> CreateSyllabusUnitAsync(UnitChapterForCreationDTO unitChapter, string syllabusUnitId)
		{
			//var syllabusDay = _repository.SyllabusUnit.s(syllabusDayId);
			// if(syllabus == null)
			//     throw new SyllabusNotFoundException(syllabusId);

			var unitChapterEntity = _mapper.Map<UnitChapter>(unitChapter);

			_repository.UnitChapter.CreateUnitChapterAsync(syllabusUnitId, unitChapterEntity);
			await _repository.SaveAsync();

			var unitChapterResult = _mapper.Map<UnitChapterDTO>(unitChapterEntity);

			return unitChapterResult;
		}
	}
}
