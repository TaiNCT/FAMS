using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nest;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.Syllabus;
using SyllabusManagementAPI.Entities.Exceptions.NotFoundException;
using Entities.Context;
using Entities.Models;
using SyllabusManagementAPI.Entities.Parameters;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SyllabusManagementAPI.Entities.Helpers;
using Azure.Core;
using Entities.DTO.SyllabusDay;

namespace SyllabusManagementAPI.Service
{
    public class SyllabusService : ISyllabusService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public SyllabusService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper, ResponseHandler responseHandler)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<ResponseDTO> GetAllSyllabusAsync(SyllabusParameters syllabusParameters)
        {
            var syllabusWithMetadata = await _repository.Syllabus.GetAllSyllabusAsync(syllabusParameters);

            var syllabusDTO = _mapper.Map<IEnumerable<SyllabusDTO>>(syllabusWithMetadata);

            ResponseDTO response = _responseHandler.GetSuccessResponse(
                $"Returned {syllabusDTO.Count()} syllabus from database.", new ResultDTO
                {
                    Data = syllabusDTO,
                    Metadata = syllabusWithMetadata.Metadata
                });

            return response;
        }

        public async Task<ResponseDTO> GetSyllabusByIdAsync(string syllabusId)
        {
            var syllabus = await _repository.Syllabus.GetSyllabusByIdAsync(syllabusId);
            if (syllabus == null)
                throw new SyllabusNotFoundException(syllabusId);

            var syllabusDTO = _mapper.Map<SyllabusDTO>(syllabus);

            ResponseDTO response = _responseHandler.GetSuccessResponse(
                $"Returned syllabus with id: '{syllabus.SyllabusId}' from database.", new ResultDTO
                {
                    Data = syllabusDTO,
                    Metadata = null
                });

            return response;
        }

        public async Task<ResponseDTO> FilterByDateSyllabusAsync(SyllabusParameters syllabusParameters, DateTime from, DateTime to)
        {
            var syllabusWithMetadata = await _repository.Syllabus.FilterByDateSyllabusAsync(syllabusParameters, from, to);

            var syllabusDTO = _mapper.Map<IEnumerable<SyllabusDTO>>(syllabusWithMetadata);

            ResponseDTO response = _responseHandler.GetSuccessResponse(
                "Filter Date Create Syllabus.", new ResultDTO
                {
                    Data = syllabusDTO,
                    Metadata = syllabusWithMetadata.Metadata
                });

            return response;
        }

        public async Task<ResponseDTO> SortSyllabusAsync(SyllabusParameters syllabusParameters, string Sortby)
        {
            var syllabusWithMetadata = await _repository.Syllabus.SortSyllabusAsync(syllabusParameters, Sortby);

            var syllabusDTO = _mapper.Map<IEnumerable<SyllabusDTO>>(syllabusWithMetadata);

            ResponseDTO response = _responseHandler.GetSuccessResponse(
                "Sort All Syllabus.", new ResultDTO
                {
                    Data = syllabusDTO,
                    Metadata = syllabusWithMetadata.Metadata
                });

            return response;
        }

        public async Task<SyllabusDTO> CreateSyllabusAsync(SyllabusForCreationDTO syllabus, bool isDraft)
        {
            var syllabusEntity = _mapper.Map<Syllabus>(syllabus);
            syllabusEntity.CreatedDate = DateTime.Now;
            syllabusEntity.Status = isDraft ? "Draft" : "Active";
			// Loop through syllabus day
			foreach (var syllabusDay in syllabusEntity.SyllabusDays)
			{
				// Loop through syllabusUnit
				foreach (var syllabusUnit in syllabusDay.SyllabusUnits)
				{
					// Loop through unitChapter
					foreach (var unitChapter in syllabusUnit.UnitChapters)
					{

						// Handle DeliveryType
						if (unitChapter.DeliveryTypeId != null)
						{
                            var deliveryType = await _repository.DeliveryType.GetDeliveryByIdAsyncV2(unitChapter.DeliveryTypeId);
							if (deliveryType == null)
							{
								throw new ArgumentException($"DeliveryTypeId '{unitChapter.DeliveryTypeId}' invalid");
							}
                            unitChapter.DeliveryType = deliveryType;
                        }

                        // Handle OutputStandard
                        if (unitChapter.OutputStandardId != null)
						{
							var outputStandard = await _repository.OutputStandard.GetOutputStandardByIdAsyncV2(unitChapter.OutputStandardId);
							if (outputStandard == null)
							{
								throw new ArgumentException($"OutputStandardId '{unitChapter.OutputStandardId}' invalid");
							}
                            unitChapter.OutputStandard = outputStandard;
                        }
					}
				}
			}
			_repository.Syllabus.Create(syllabusEntity);
            await _repository.SaveAsync();

            var syllabusToReturn = _mapper.Map<SyllabusDTO>(syllabusEntity);

            return syllabusToReturn;
        }

        public async Task<SyllabusDTO> UpdateSyllabusAsync(SyllabusForUpdateDTO syllabus)
        {
            var findSyllabus = await _repository.Syllabus.GetSyllabusByIdAsyncV2(syllabus.SyllabusId);
            if (findSyllabus == null) { throw new SyllabusNotFoundException(syllabus.SyllabusId); }
            _mapper.Map(syllabus, findSyllabus);
            findSyllabus.ModifiedBy = "Em phuc le";
            findSyllabus.ModifiedDate = DateTime.Now;
            _repository.Syllabus.UpdateSyllabus(findSyllabus);
            // Update or Create AssessmentScheme
            _repository.AssessmentSchemeRepository.DeleteAssessmentSchemesAsync(syllabus.SyllabusId);
            foreach (var assessmentSchemeDTO in syllabus.AssessmentSchemes)
            {
                var editAssessmentScheme = _mapper.Map<AssessmentScheme>(assessmentSchemeDTO);
                _repository.AssessmentSchemeRepository.CreateAssessmentSchemeAsync(syllabus.SyllabusId, editAssessmentScheme);
            }
            //Update or Create Syllabus Day
            _repository.SyllabusDay.DeleteSyllabusDaysAsync(syllabus.SyllabusId);
            foreach (var syllabusDayDTO in syllabus.SyllabusDays)
            {
                var editsyllabusDay = _mapper.Map<SyllabusDay>(syllabusDayDTO);
                _repository.SyllabusDay.CreateSyllabusDayAsync(syllabus.SyllabusId, editsyllabusDay);
            }
            await _repository.SaveAsync();
            var syllabusToReturn = _mapper.Map<SyllabusDTO>(findSyllabus);
            return syllabusToReturn;
        }

        public async Task<ResponseDTO> DuplicateSyllabusAsync(DuplicateSyllabusRequest request)
		{
            //check if code is empty
            if (string.IsNullOrEmpty(request.SyllabusId))
                throw new ArgumentException($"The code {request.SyllabusId} of training program is empty");
			var original = await _repository.Syllabus.GetSyllabusByIdAsyncV2(request.SyllabusId);
            var duplicateSyllabusDto = _mapper.Map<SyllabusForCreationDTO>(original);
            var syllabusEntity = _mapper.Map<Syllabus>(duplicateSyllabusDto);
			if (syllabusEntity is not null)
			{
				// create duplicate syllabus
				syllabusEntity.CreatedDate = DateTime.Now;
				syllabusEntity.CreatedBy = request.CreatedBy;
                syllabusEntity.TopicCode = request.TopicCode;
				syllabusEntity.Status = "Draft";
				_repository.Syllabus.Create(syllabusEntity);
                await _repository.SaveAsync();
			}
			if (syllabusEntity is not null)
			{
				// get the duplicate program
				syllabusEntity = await _repository.Syllabus.FindByCondition(x => x.TopicName.Equals(syllabusEntity!.TopicName))
				.OrderByDescending(x => x.Id)
				.FirstOrDefaultAsync();
			}
            var syllabusDto = _mapper.Map<SyllabusDTO>(syllabusEntity);
			ResponseDTO response = _responseHandler.GetSuccessResponse(
				$"Returned syllabus with id: '{syllabusEntity!.SyllabusId}' from database duplicated.", new ResultDTO
				{
					Data = syllabusDto,
					Metadata = null
				});

			return response;
		}

		public async Task DeleteSyllabusAsync(string syllabusId)
        {
            var syllabus = await _repository.Syllabus.GetSyllabusByIdAsync(syllabusId);
            if (syllabus == null)
                throw new SyllabusNotFoundException(syllabusId);

            _repository.Syllabus.DeleteSyllabus(syllabus);
            await _repository.SaveAsync();
        }

        public async Task<ResponseDTO> GetHeaderAsync(string syllabusId)
        {
            var syllabus = await _repository.Syllabus.GetSyllabusByIdAsync(syllabusId);
            if (syllabus == null)
                throw new SyllabusNotFoundException(syllabusId);

            var headerResult = new ViewDTO.HeaderViewModel
            {
                TopicCode = syllabus.TopicCode,
                TopicName = syllabus.TopicName,
                Version = syllabus.Version,
                ModifiedBy = syllabus.ModifiedBy,
                ModifiedDate = syllabus?.ModifiedDate,
                Status = syllabus.Status,
            };

            // var headerResult = _mapper.Map<ViewDTO.HeaderViewModel>(syllabus);

            ResponseDTO response = _responseHandler.GetSuccessResponse(
                $"Returned syllabus with id: '{syllabus.SyllabusId}' from database.", new ResultDTO
                {
                    Data = headerResult,
                    Metadata = null
                });

            return response;
        }

        public async Task<ResponseDTO> GetGeneralAsync(string syllabusId)
        {
            var syllabus = await _repository.Syllabus.GetOutputStandardBySyllabusIdAsync(syllabusId);

            if (syllabus == null)
                throw new SyllabusNotFoundException(syllabusId);

            var generalResult = new ViewDTO.GeneralViewModel
            {
                AttendeeNumber = syllabus?.AttendeeNumber,
                Level = syllabus?.Level,
                TechnicalRequirement = syllabus?.TechnicalRequirement,
                CourseObjective = syllabus?.CourseObjective,
                Days = syllabus?.Days,
                Hours = syllabus?.Hours,
                OutputStandardCode = syllabus?.SyllabusDays?.SelectMany(sd => sd
                   .SyllabusUnits.SelectMany(su => su
                   .UnitChapters)).Select(uc => uc
                   .OutputStandard?.Code).FirstOrDefault()
            };

            ResponseDTO response = _responseHandler.GetSuccessResponse("Get general success.", new ResultDTO
            {
                Data = generalResult,
                Metadata = null
            });

            return response;
        }
        

        public async Task<ResponseDTO> GetDeliveryTypePercentages(string syllabusId)
        {
            var percentage = await _repository.Syllabus.GetDeliveryTypePercentages(syllabusId);

            ResponseDTO response = _responseHandler.GetSuccessResponse("Get delivery type percentages success.", new ResultDTO
            {
                Data = percentage,
                Metadata = null
            });

            return response;
        }

		public async Task ActivateDeactivateSyllabus(string syllabusId, [FromBody] bool activate)
		{
			var syllabus = await _repository.Syllabus.GetSyllabusByIdAsync(syllabusId);

			if (syllabus == null)
				throw new SyllabusNotFoundException(syllabusId);

			syllabus.Status = activate ? "Active" : "Inactive";
			_repository.Syllabus.Update(syllabus);
			await _repository.SaveAsync();
		}

		public async Task<SyllabusDTO> ImportSyllabus(IFormFile file)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			SyllabusForCreationDTO syllabus = null;

			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					var rowCount = worksheet.Dimension.Rows;

					syllabus = new SyllabusForCreationDTO
					{
						TopicCode = ExcelHelper.ReadExcelCell(worksheet, 3, 3),
						TopicName = ExcelHelper.ReadExcelCell(worksheet, 2, 3),
						Version = ExcelHelper.ReadExcelCell(worksheet, 4, 3),
						AttendeeNumber = int.Parse(ExcelHelper.ReadExcelCell(worksheet, 5, 3) ?? "0"),
						Level = ExcelHelper.ReadExcelCell(worksheet, 6, 3),
						CourseObjective = ExcelHelper.ReadExcelCell(worksheet, 15, 3),
						TechnicalRequirement = ExcelHelper.ReadExcelCell(worksheet, 18, 4),
						CreatedBy = "",
						DeliveryPrinciple = "<div>" + ExcelHelper.CombineRowsAndCells(worksheet, 25, 31, 3, 4) + "</div>"
					};
				}
			}

			var syllabusReponse = await CreateSyllabusImportAsync(syllabus);

			return syllabusReponse;
		}

		public async Task<SyllabusDTO> CreateSyllabusImportAsync(SyllabusForCreationDTO syllabus)
		{
			var syllabusEntity = _mapper.Map<Syllabus>(syllabus);
			syllabusEntity.CreatedDate = DateTime.Now;
            syllabusEntity.ModifiedDate = DateTime.Now;
            syllabusEntity.Status = "Draft";
			_repository.Syllabus.CreateSyllabus(syllabusEntity);
			await _repository.SaveAsync();

			var syllabusResult = _mapper.Map<SyllabusDTO>(syllabusEntity);

			return syllabusResult;
		}

		public async Task<(SyllabusDTO, bool)> HandleDuplicate([FromForm] DuplicateHandlingDTO model)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			string topicName = null;
			bool check = false;
            SyllabusDTO syllabus = null;
			using (var stream = new MemoryStream())
			{
				await model.File.CopyToAsync(stream);
				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					var rowCount = worksheet.Dimension.Rows;
					topicName = ExcelHelper.ReadExcelCell(worksheet, 2, 3);
				}
			}

			switch (model.DuplicateHandling)
			{
				case "Allow":	syllabus = await this.ImportSyllabus(model.File);
								check = true;
								break;

				case "Replace": await _repository.Syllabus.DeleteDuplicateSyllabuses(topicName);
					            syllabus = await this.ImportSyllabus(model.File);
					            check = true;
					            break;

				default:		check = false;
								break;
			}
			return (syllabus,check);
		}

		public async Task<ResponseDTO> SearchAsync(string keywords, SyllabusParameters syllabusParameters)
		{
			var syllabusMetadata = await _repository.Syllabus.Search(keywords, syllabusParameters);

			var syllabusDTO = _mapper.Map<IEnumerable<SyllabusDTO>>(syllabusMetadata);

			ResponseDTO response = _responseHandler.GetSuccessResponse(
				$"Returned syllabus for keywords '{string.Join(", ", keywords)}' from Elasticsearch.",
				new ResultDTO
				{
					Data = syllabusDTO,
					Metadata = syllabusMetadata.Metadata
				});

			return response;
		}
	}
}
