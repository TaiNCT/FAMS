using AutoMapper;
using OfficeOpenXml;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.SyllabusDay;
using SyllabusManagementAPI.Entities.DTO.SyllabusUnit;
using SyllabusManagementAPI.Entities.Helpers;
using Entities.Models;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Service
{
	public class SyllabusUnitService: ISyllabusUnitService
	{
		private readonly IRepositoryWrapper _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly ResponseHandler _responseHandler;

		public SyllabusUnitService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper, ResponseHandler responseHandler)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}

		public async Task<SyllabusUnitDTO> CreateSyllabusUnitAsync(SyllabusUnitForCreationDTO syllabusUnit, string syllabusDayId)
		{
			var syllabusDay = _repository.SyllabusDay.GetSyllabusDaysBySyllabusIdAsync(syllabusDayId);
			// if(syllabus == null)
			//     throw new SyllabusNotFoundException(syllabusId);

			var syllabusUnitEntity = _mapper.Map<SyllabusUnit>(syllabusUnit);

			_repository.SyllabusUnit.CreateSyllabusUnitAsync(syllabusDayId, syllabusUnitEntity);
			await _repository.SaveAsync();

			var syllabusUnitResult = _mapper.Map<SyllabusUnitDTO>(syllabusUnitEntity);

			return syllabusUnitResult;
		}
	}
}
