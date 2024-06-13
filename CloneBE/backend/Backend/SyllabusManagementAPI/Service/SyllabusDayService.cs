using AutoMapper;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.Exceptions.NotFoundException;
using Entities.Models;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;
using OfficeOpenXml;
using Entities.DTO.SyllabusDay;
using SyllabusManagementAPI.Entities.Helpers;
using SyllabusManagementAPI.Entities.DTO.SyllabusUnit;

namespace SyllabusManagementAPI.Service
{
	public class SyllabusDayService : ISyllabusDayService
	{
		private readonly IRepositoryWrapper _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly ResponseHandler _responseHandler;

		public SyllabusDayService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper, ResponseHandler responseHandler)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}

		public async Task<SyllabusDayDTO> CreateSyllabusDayAsync(SyllabusDayForCreationDTO syllabusDay, string syllabusId)
		{
			var syllabus = _repository.Syllabus.GetSyllabusByIdAsync(syllabusId);
			// if(syllabus == null)
			//     throw new SyllabusNotFoundException(syllabusId);

			var syllabusDayEntity = _mapper.Map<SyllabusDay>(syllabusDay);

			_repository.SyllabusDay.CreateSyllabusDayAsync(syllabusId, syllabusDayEntity);
			await _repository.SaveAsync();

			var syllabusDayResult = _mapper.Map<SyllabusDayDTO>(syllabusDay);

			return syllabusDayResult;
		}

		public async Task<ResponseDTO> GetSyllabusDaysOutlineBySyllabusIdAsync(string syllabusId)
		{
			var syllabusDays = await _repository.SyllabusDay.GetSyllabusDaysOutlineBySyllabusIdAsync(syllabusId);
			if (syllabusDays == null)
				throw new AssessmentSchemeNotFoundException(syllabusId);

			var outlineResult = syllabusDays.Select(sd => new ViewDTO.SyllabusDayViewModel
			{
				DayNo = sd.DayNo.Value,
				SyllabusUnits = sd.SyllabusUnits.Select(su => new ViewDTO.SyllabusUnitViewModel
				{
					UnitNo = su.UnitNo,
					Name = su.Name,
					Duration = su.Duration,
					UnitChapters = su.UnitChapters.Select(uc => new ViewDTO.UnitChapterViewModel
					{
						Name = uc.Name,
						Duration = uc.Duration,
						IsOnline = uc.IsOnline,
						DeliveryTypeId = uc.DeliveryTypeId,
						OutputStandardId = uc.OutputStandardId,
						DeliveryTypeName = uc.DeliveryType.Name,
						OutputStandardName = uc.OutputStandard.Code,

					}).ToList()
				}).ToList()
			}).ToList();

			ResponseDTO response = _responseHandler.GetSuccessResponse("Get general success.", new ResultDTO
			{
				Data = outlineResult,
				Metadata = null
			});
			return response;
		}

		public async Task ImportSyllabusDay(string syllabusId, IFormFile file)
		{
			List<SyllabusDayForCreationDTO> syllabusDays = null;
			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets["<Topics Code>_ScheduleDetail"];
					int index = 3;
					syllabusDays = new List<SyllabusDayForCreationDTO>();
					string UnitName = ExcelHelper.ReadExcelCell(worksheet, index, 2);
					while (UnitName != null)
					{
						SyllabusDayForCreationDTO syllabusDay = new SyllabusDayForCreationDTO();
						// Parsing DayNo
						string dayNoString = ExcelHelper.ReadExcelCell(worksheet, index, 3);
						int dayNo;
						if (int.TryParse(dayNoString, out dayNo))
						{
							syllabusDay.DayNo = dayNo;
						}
						else
						{
							syllabusDay.DayNo = 1;
						}
						syllabusDays.Add(syllabusDay);
						int count = ExcelHelper.GetMergedCellCount(worksheet, index, 2);
						List<SyllabusUnitForCreationDTO> syllabusUnits = new List<SyllabusUnitForCreationDTO>();

						SyllabusUnitForCreationDTO syllabusUnit = new SyllabusUnitForCreationDTO();
						syllabusUnit.UnitNo = int.Parse(ExcelHelper.ReadExcelCell(worksheet, index, 1));
						syllabusUnit.Name = UnitName;
						syllabusUnit.Duration = 3;
						syllabusUnits.Add(syllabusUnit);

						List<UnitChapterForCreationDTO> unitChapters = new List<UnitChapterForCreationDTO>();
						for (int i = 0; i < count; i++)
						{
							UnitChapterForCreationDTO unitChapter = new UnitChapterForCreationDTO();
							unitChapter.ChapterNo = i+1;
							unitChapter.Name = ExcelHelper.ReadExcelCell(worksheet, index, 4);
							unitChapter.Duration = int.Parse(ExcelHelper.ReadExcelCell(worksheet, index, 7));
							unitChapter.IsOnline = false ? ExcelHelper.ReadExcelCell(worksheet, index, 8) == "Offline" : ExcelHelper.ReadExcelCell(worksheet, index, 8) == "Online";
							unitChapter.OutputStandardId = "outputStandardId1";
							unitChapter.DeliveryTypeId = "deliveryTypeId1";
							unitChapters.Add(unitChapter);
							index++;
						}
						syllabusDay.SyllabusUnits = syllabusUnits.ToList();
						syllabusUnit.UnitChapters = unitChapters.ToList();
						UnitName = ExcelHelper.ReadExcelCell(worksheet, index, 2);
					}
				}
			}

			foreach (SyllabusDayForCreationDTO nSyllabusDay in syllabusDays)
			{
				var impSyllabusDay = _mapper.Map<SyllabusDay>(nSyllabusDay);
				impSyllabusDay.CreatedDate = DateTime.Now;
				impSyllabusDay.ModifiedDate = DateTime.Now;
				_repository.SyllabusDay.CreateSyllabusDayAsync(syllabusId, impSyllabusDay);
			}
			await _repository.SaveAsyncV1();
		}
	}
}