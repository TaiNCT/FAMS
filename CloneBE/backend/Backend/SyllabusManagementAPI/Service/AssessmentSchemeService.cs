using AutoMapper;
using OfficeOpenXml;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using Entities.Models;
using SyllabusManagementAPI.Entities.Exceptions.NotFoundException;
using SyllabusManagementAPI.Entities.Helpers;
using Entities.Context;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Service
{
    public class AssessmentSchemeService : IAssessmentSchemeService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public AssessmentSchemeService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper, ResponseHandler responseHandler)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<ResponseDTO> GetAssessmentSchemeByIdAsync(string syllabusId)
        {
            var assessmentScheme = await _repository.AssessmentSchemeRepository.GetAssessmentSchemeByIdAsync(syllabusId);

            //if (assessmentScheme == null)
            //    throw new AssessmentSchemeNotFoundException(syllabusId);

            var assessmentResult = new ViewDTO.AssessmentSchemeViewModel
            {
                Assignment = assessmentScheme.Assignment,
                FinalPractice = assessmentScheme.FinalPractice,
                Final = assessmentScheme.Final,
                FinalTheory = assessmentScheme.FinalTheory,
                Gpa = assessmentScheme.Gpa,
                Quiz = assessmentScheme.Quiz,
                DeliveryPrinciple = assessmentScheme.Syllabus.DeliveryPrinciple
            };

            // var assessmentResult = _mapper.Map<ViewDTO.AssessmentSchemeViewModel>(assessmentScheme);

            ResponseDTO response = _responseHandler.GetSuccessResponse(
                $"Returned AssessmentScheme with syllabus id: '{assessmentScheme.SyllabusId}' from database.", new ResultDTO
                {
                    Data = assessmentResult,
                    Metadata = null
                });

            return response;
        }

        public async Task<AssessmentSchemeDTO> CreateAssessmentSchemeAsync(string syllabusId, AssessmentSchemeDTO assessmentScheme)
        {
            var syllabus = _repository.Syllabus.GetSyllabusByIdAsync(syllabusId);
            // if(syllabus == null)
            //     throw new SyllabusNotFoundException(syllabusId);

            var assessmentSchemeEntity = _mapper.Map<AssessmentScheme>(assessmentScheme);

            _repository.AssessmentSchemeRepository.CreateAssessmentSchemeAsync(syllabusId, assessmentSchemeEntity);
            await _repository.SaveAsync();

            var assessmentSchemeResult = _mapper.Map<AssessmentSchemeDTO>(assessmentSchemeEntity);

            return assessmentSchemeResult;
        }
		public async Task<AssessmentSchemeDTO> ImportAssessmentScheme(string syllabusId, IFormFile file)
		{
			var syllabus = _repository.Syllabus.GetSyllabusByIdAsync(syllabusId);
			// if(syllabus == null)
			//     throw new SyllabusNotFoundException(syllabusId);
			AssessmentSchemeForCreationDTO assessmentSheme = null;
			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					var rowCount = worksheet.Dimension.Rows;
					assessmentSheme = new AssessmentSchemeForCreationDTO()
					{
						Assignment = float.Parse(ExcelHelper.ReadExcelCell(worksheet, 20, 4)),
						Quiz = float.Parse(ExcelHelper.ReadExcelCell(worksheet, 19, 4)),
						FinalTheory = float.Parse(ExcelHelper.ReadExcelCell(worksheet, 21, 4)),
						FinalPractice = float.Parse(ExcelHelper.ReadExcelCell(worksheet, 22, 4)),
						Gpa = float.Parse(ExcelHelper.ReadExcelCell(worksheet, 23, 4)),
						Final = float.Parse(ExcelHelper.ReadExcelCell(worksheet, 24, 4))
					};
				}
			}
			var assessmentScheme = _mapper.Map<AssessmentScheme>(assessmentSheme);

			_repository.AssessmentSchemeRepository.CreateAssessmentSchemeAsync(syllabusId, assessmentScheme);
			await _repository.SaveAsyncV1();

			var assessmentSchemeResult = _mapper.Map<AssessmentSchemeDTO>(assessmentScheme);

			return assessmentSchemeResult;
		}
	}
}
