using AutoMapper;
using OfficeOpenXml;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.Helpers;
using Entities.Models;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Service
{
	public class OutputStandardService : IOutputStandardService
	{
		private readonly IRepositoryWrapper _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly ResponseHandler _responseHandler;

		public OutputStandardService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper, ResponseHandler responseHandler)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
			_responseHandler = responseHandler;
		}
		public async Task<OutputStandardForCreationDTO> CreateOutputStandardAsync(OutputStandardForCreationDTO outputStandard)
		{
			var outputStandardEntity = _mapper.Map<OutputStandard>(outputStandard);

			_repository.OutputStandard.CreateOutputStandardAsync(outputStandardEntity);
			await _repository.SaveAsync();

			var outputStandardResult = _mapper.Map<OutputStandardForCreationDTO>(outputStandardEntity);

			return outputStandardResult;
		}

		public async Task<OutputStandardForCreationDTO> ImportOutputStandard(IFormFile file)
		{
			OutputStandardForCreationDTO outputStandardResult = null;
			List<OutputStandardForCreationDTO> outputStandards = new List<OutputStandardForCreationDTO>();

			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					var rowCount = worksheet.Dimension.Rows;
					for (int row = 8; row <= 14; row++)
					{
						string checkBlank = ExcelHelper.ReadExcelCell(worksheet, row, 3);
						if (checkBlank != null)
						{
							var outputStandard = new OutputStandardForCreationDTO()
							{
								Name = ExcelHelper.ReadExcelCell(worksheet, row, 3),
								Code = ExcelHelper.ReadExcelCell(worksheet, row, 4),
								Descriptions = ExcelHelper.ReadExcelCell(worksheet, row, 5)
							};
							outputStandards.Add(outputStandard);
						}
						else
						{
							break;
						}
					}
				}
			}

			foreach (var outputStandard in outputStandards)
			{
				var outputStandardEntity = _mapper.Map<OutputStandard>(outputStandard);
				_repository.OutputStandard.CreateOutputStandardAsync(outputStandardEntity);
			}

			await _repository.SaveAsyncV1();

			// Assuming you want to return the last output standard imported
			if (outputStandards.Count > 0)
			{
				outputStandardResult = outputStandards[outputStandards.Count - 1];
			}

			return outputStandardResult;
		}
	}
}
