using AutoMapper;
using Azure;
using Azure.Core;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using Entities.Context;
using Entities.Models;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using Nest;
using StudentInfoManagementAPI.DTO;
using System.Data;

namespace StudentInfoManagementAPI.Service
{
	public class UploadFileDL : IUploadFileDL
	{
		public readonly IConfiguration _configuration;
		public readonly FamsContext _dbContext;
		List<Student> studentParameters = new List<Student>();
		List<StudentClass> studentClassParameters = new List<StudentClass>();
		private readonly IElasticClient _elasticClient;
		private IMapper _mapper;

		public UploadFileDL(IConfiguration configuration, FamsContext dbcontext, IElasticClient elasticClient, IMapper mapper)
		{
			_configuration = configuration;
			_dbContext = dbcontext;
			_elasticClient = elasticClient;
			_mapper = mapper;
		}

		public async Task<UploadExcelFileResponse> UploadXMLFile(UploadExcelFileRequest request, string Path)
		{
			UploadExcelFileResponse response = new UploadExcelFileResponse();
			DataSet dataSet;
			response.IsSuccess = true;
			response.Message = "Data upload successfully";

			try
			{
				if (request.File.FileName.ToLower().Contains(".xlsx"))
				{
					using (FileStream stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
						using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
						{
							dataSet = reader.AsDataSet(new ExcelDataSetConfiguration { ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true } });
						}
					}

					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						Student studentParameter = new Student
						{
							MutatableStudentId = row[0] != DBNull.Value ? Convert.ToString(row[0]) : null,
							CertificationStatus = row[1] != DBNull.Value ? Convert.ToBoolean(row[1]) : false,
							FullName = row[2] != DBNull.Value ? Convert.ToString(row[2]) : null,
							Dob = row[3] != DBNull.Value ? Convert.ToDateTime(row[3]) : DateTime.MinValue,
							Gender = row[4] != DBNull.Value ? Convert.ToString(row[4]) : null,
							Phone = row[5] != DBNull.Value ? Convert.ToString(row[5]) : null,
							Email = row[6] != DBNull.Value ? Convert.ToString(row[6]) : null,
							MajorId = row[7] != DBNull.Value ? Convert.ToString(row[7]) : null,
							GraduatedDate = row[8] != DBNull.Value ? Convert.ToDateTime(row[8]) : DateTime.MinValue,
							Gpa = row[9] != DBNull.Value ? Convert.ToDecimal(row[9]) : 0,
							Address = row[10] != DBNull.Value ? Convert.ToString(row[10]) : null,
							Faaccount = row[11] != DBNull.Value ? Convert.ToString(row[11]) : null,
							Type = row[12] != DBNull.Value ? Convert.ToInt32(row[12]) : -1,
							Status = row[13] != DBNull.Value ? Convert.ToString(row[13]) : null,
							JoinedDate = row[14] != DBNull.Value ? Convert.ToDateTime(row[14]) : DateTime.MinValue,
							Area = row[15] != DBNull.Value ? Convert.ToString(row[15]) : null,
							Recer = row[16] != DBNull.Value ? Convert.ToString(row[16]) : null,
							University = row[17] != DBNull.Value ? Convert.ToString(row[17]) : null,
							Audit = row[18] != DBNull.Value ? Convert.ToInt32(row[18]) : -1,
							Mock = row[19] != DBNull.Value ? Convert.ToInt32(row[19]) : -1,
						};

						studentParameters.Add(studentParameter);
						_dbContext.Students.Add(studentParameter);

						await _dbContext.SaveChangesAsync();

						StudentClass studentClassParameter = new StudentClass
						{
							StudentId = studentParameter.StudentId,
							AttendingStatus = row[20] != DBNull.Value ? Convert.ToString(row[20]) : null,
							Result = row[21] != DBNull.Value ? Convert.ToInt32(row[21]) : -1,
							FinalScore = row[22] != DBNull.Value ? Convert.ToDecimal(row[22]) : 0,
							Gpalevel = row[23] != DBNull.Value ? Convert.ToInt32(row[23]) : -1,
							CertificationStatus = row[24] != DBNull.Value ? Convert.ToInt32(row[24]) : -1,
							CertificationDate = row[25] != DBNull.Value ? Convert.ToDateTime(row[25]) : DateTime.MinValue,
							Method = row[26] != DBNull.Value ? Convert.ToInt32(row[26]) : -1,
							ClassId = request.classId,
						};

						studentClassParameters.Add(studentClassParameter);
						_dbContext.StudentClasses.Add(studentClassParameter);

						await _dbContext.SaveChangesAsync();
					}

					try
					{
						var result = DeleteDataElastic();
						if (result)
						{
							var getAllStudent = _dbContext.Students.Include(s => s.StudentClasses).Include(s => s.Major).ToList();
							var studentDtos = getAllStudent.Select(
								student => new StudentDTO
								{
									StudentInfoDTO = _mapper.Map<StudentInfoDTO>(student),
									MajorDTO = _mapper.Map<MajorDTO>(student.Major),
									StudentClassDTOs = _mapper.Map<IEnumerable<StudentClassDTO>>(student.StudentClasses.ToList())
								}).ToList();

							foreach (var student in studentDtos)
							{
								var responses = await _elasticClient.IndexDocumentAsync(student);
								if (!responses.IsValid)
								{
									response.IsSuccess = false;
									response.Message = $"Failed to index user {student.StudentInfoDTO.StudentId}: {responses.ServerError.Error.Reason}";
									return response;
								}
							}
						}
						else
						{
							response.IsSuccess = false;
							response.Message = "Failed to delete old data";
						}
					}
					catch (Exception ex)
					{
						response.IsSuccess = false;
						response.Message = ex.Message;
					}
				}
				else
				{
					response.IsSuccess = false;
					response.Message = "Invalid file format";
				}
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task UploadOption(UploadExcelFileRequest request, string Path)
		{
			UploadExcelFileResponse response = new UploadExcelFileResponse();
			DataSet dataSet;

			try
			{
				if (request.File.FileName.ToLower().Contains(".xlsx"))
				{
					using (FileStream stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
						using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
						{
							dataSet = reader.AsDataSet(new ExcelDataSetConfiguration { ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true } });
						}
					}

					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						Student studentParameter = new Student
						{
							MutatableStudentId = row[0] != DBNull.Value ? Convert.ToString(row[0]) : null,
							CertificationStatus = row[1] != DBNull.Value ? Convert.ToBoolean(row[1]) : false,
							FullName = row[2] != DBNull.Value ? Convert.ToString(row[2]) : null,
							Dob = row[3] != DBNull.Value ? Convert.ToDateTime(row[3]) : DateTime.MinValue,
							Gender = row[4] != DBNull.Value ? Convert.ToString(row[4]) : null,
							Phone = row[5] != DBNull.Value ? Convert.ToString(row[5]) : null,
							Email = row[6] != DBNull.Value ? Convert.ToString(row[6]) : null,
							MajorId = row[7] != DBNull.Value ? Convert.ToString(row[7]) : null,
							GraduatedDate = row[8] != DBNull.Value ? Convert.ToDateTime(row[8]) : DateTime.MinValue,
							Gpa = row[9] != DBNull.Value ? Convert.ToDecimal(row[9]) : 0,
							Address = row[10] != DBNull.Value ? Convert.ToString(row[10]) : null,
							Faaccount = row[11] != DBNull.Value ? Convert.ToString(row[11]) : null,
							Type = row[12] != DBNull.Value ? Convert.ToInt32(row[12]) : -1,
							Status = row[13] != DBNull.Value ? Convert.ToString(row[13]) : null,
							JoinedDate = row[14] != DBNull.Value ? Convert.ToDateTime(row[14]) : DateTime.MinValue,
							Area = row[15] != DBNull.Value ? Convert.ToString(row[15]) : null,
							Recer = row[16] != DBNull.Value ? Convert.ToString(row[16]) : null,
							University = row[17] != DBNull.Value ? Convert.ToString(row[17]) : null,
							Audit = row[18] != DBNull.Value ? Convert.ToInt32(row[18]) : -1,
							Mock = row[19] != DBNull.Value ? Convert.ToInt32(row[19]) : -1,
						};

						studentParameters.Add(studentParameter);

						var existingStudent = await _dbContext.Students.FirstOrDefaultAsync(s => s.Faaccount == studentParameter.Faaccount);

						if (existingStudent == null)
						{
							await _dbContext.Students.AddAsync(studentParameter);
							await _dbContext.SaveChangesAsync();
							existingStudent = studentParameter;
						}

						StudentClass studentClassParameter = new StudentClass
						{
							StudentId = _dbContext.Students.First(s => s.Faaccount == (row[11] != DBNull.Value ? Convert.ToString(row[11]) : null)).StudentId,
							AttendingStatus = row[20] != DBNull.Value ? Convert.ToString(row[20]) : null,
							Result = row[21] != DBNull.Value ? Convert.ToInt32(row[21]) : -1,
							FinalScore = row[22] != DBNull.Value ? Convert.ToDecimal(row[22]) : 0,
							Gpalevel = row[23] != DBNull.Value ? Convert.ToInt32(row[23]) : -1,
							CertificationStatus = row[24] != DBNull.Value ? Convert.ToInt32(row[24]) : -1,
							CertificationDate = row[25] != DBNull.Value ? Convert.ToDateTime(row[25]) : DateTime.MinValue,
							Method = row[26] != DBNull.Value ? Convert.ToInt32(row[26]) : -1,
							ClassId = request.classId,
						};
						studentClassParameters.Add(studentClassParameter);

						var existingStudentClass = await _dbContext.StudentClasses.FirstOrDefaultAsync(sc => sc.StudentId == studentClassParameter.StudentId && sc.ClassId == request.classId);

						if (existingStudentClass == null)
						{
							await _dbContext.StudentClasses.AddAsync(studentClassParameter);
							await _dbContext.SaveChangesAsync();
							existingStudentClass = studentClassParameter;
						}
					}

					try
					{
						var result = DeleteDataElastic();
						if (result)
						{
							var getAllStudent = _dbContext.Students.Include(s => s.StudentClasses).Include(s => s.Major).ToList();
							var studentDtos = getAllStudent.Select(
								student => new StudentDTO
								{
									StudentInfoDTO = _mapper.Map<StudentInfoDTO>(student),
									MajorDTO = _mapper.Map<MajorDTO>(student.Major),
									StudentClassDTOs = _mapper.Map<IEnumerable<StudentClassDTO>>(student.StudentClasses.ToList())
								}).ToList();

							foreach (var student in studentDtos)
							{
								var responses = await _elasticClient.IndexDocumentAsync(student);
								if (!responses.IsValid)
								{
									response.IsSuccess = false;
									response.Message = $"Failed to index user {student.StudentInfoDTO.StudentId}: {responses.ServerError.Error.Reason}";
								}
							}
						}
						else
						{
							response.IsSuccess = false;
							response.Message = "Failed to delete old data";
						}
					}
					catch (Exception ex)
					{
						response.IsSuccess = false;
						response.Message = ex.Message;
					}
				}
			}
			catch (Exception e)
			{
				response.IsSuccess = false;
				response.Message = e.Message;
			}
		}

		private bool IsDuplicateData(string row)
		{
			return _dbContext.Students.Any(fa => fa.Faaccount == row);
		}

		public bool DeleteDataElastic()
		{
			var response = _elasticClient.Indices.Delete("students");
			return response.IsValid;
		}

		public async Task<bool> HasDuplicates(IFormFile file)
		{
			DataSet dataSet;
			using (MemoryStream stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				stream.Position = 0;
				System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
				using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
				{
					dataSet = reader.AsDataSet();
				}
			}

			HashSet<Student> uniqueRecords = new HashSet<Student>();
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				string indentifier = row[11].ToString();
				if (IsDuplicateData(indentifier))
					return true;
			}
			return false;
		}

		public async Task HandleReplaceOption(string classId)
		{
			foreach (var studentParameter in studentParameters)
			{
				var existingStudents = await _dbContext.Students
					.Include(sc => sc.StudentClasses.Where(sc => sc.ClassId == classId))
					.Where(s => s.Faaccount == studentParameter.Faaccount).ToListAsync();

				if (existingStudents != null)
				{
					var studentClass = studentClassParameters.FirstOrDefault(sc => sc.ClassId == classId && sc.StudentId == existingStudents.First().StudentId);

					foreach (var existingStudent in existingStudents)
					{
						existingStudent.MutatableStudentId = studentParameter.MutatableStudentId;
						existingStudent.CertificationStatus = studentParameter.CertificationStatus;
						existingStudent.FullName = studentParameter.FullName;
						existingStudent.Dob = studentParameter.Dob;
						existingStudent.Gender = studentParameter.Gender;
						existingStudent.Phone = studentParameter.Phone;
						existingStudent.Email = studentParameter.Email;
						existingStudent.MajorId = studentParameter.MajorId;
						existingStudent.GraduatedDate = studentParameter.GraduatedDate;
						existingStudent.Gpa = studentParameter.Gpa;
						existingStudent.Address = studentParameter.Address;
						existingStudent.Type = studentParameter.Type;
						existingStudent.Status = studentParameter.Status;
						existingStudent.JoinedDate = studentParameter.JoinedDate;
						existingStudent.Recer = studentParameter.Recer;
						existingStudent.University = studentParameter.University;
						existingStudent.Audit = studentParameter.Audit;
						existingStudent.Mock = studentParameter.Mock;

						if (studentClass != null && existingStudent.StudentClasses.Count > 0)
						{
							existingStudent.StudentClasses.First().AttendingStatus = studentClass.AttendingStatus;
							existingStudent.StudentClasses.First().Result = studentClass.Result;
							existingStudent.StudentClasses.First().FinalScore = studentClass.FinalScore;
							existingStudent.StudentClasses.First().Gpalevel = studentClass.Gpalevel;
							existingStudent.StudentClasses.First().CertificationStatus = studentClass.CertificationStatus;
							existingStudent.StudentClasses.First().CertificationDate = studentClass.CertificationDate;
							existingStudent.StudentClasses.First().Method = studentClass.Method;
						}

						await _dbContext.SaveChangesAsync();
					}
				}

				var searchResponse = await _elasticClient.SearchAsync<StudentDTO>(s => s
				   .Query(q => q.Match(m => m.Field(f => f.StudentInfoDTO.StudentId).Query(studentParameter.StudentId)))
				);

				if (searchResponse.IsValid && searchResponse.Documents.Any())
				{
					foreach (var hit in searchResponse.Hits)
					{
						var studentDto = hit.Source;
						studentDto.StudentInfoDTO.FullName = studentParameter.FullName;
						studentDto.StudentInfoDTO.Dob = studentParameter.Dob;
						studentDto.StudentInfoDTO.Gender = studentParameter.Gender;
						studentDto.StudentInfoDTO.Phone = studentParameter.Phone;
						studentDto.StudentInfoDTO.Email = studentParameter.Email;
						studentDto.StudentInfoDTO.MajorId = studentParameter.MajorId;
						studentDto.StudentInfoDTO.GraduatedDate = studentParameter.GraduatedDate;
						studentDto.StudentInfoDTO.Gpa = studentParameter.Gpa;
						studentDto.StudentInfoDTO.Address = studentParameter.Address;
						studentDto.StudentInfoDTO.Type = studentParameter.Type;
						studentDto.StudentInfoDTO.Status = studentParameter.Status;
						studentDto.StudentInfoDTO.JoinedDate = studentParameter.JoinedDate;
						studentDto.StudentInfoDTO.Recer = studentParameter.Recer;
						studentDto.StudentInfoDTO.University = studentParameter.University;

						var studentClassDto = studentDto.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == classId && sc.StudentId == studentParameter.StudentId);
						if (studentClassDto != null)
						{
							var existingStudentClass = studentParameter.StudentClasses.First();
							studentClassDto.AttendingStatus = existingStudentClass.AttendingStatus;
							studentClassDto.Result = existingStudentClass.Result;
							studentClassDto.FinalScore = existingStudentClass.FinalScore;
							studentClassDto.Gpalevel = existingStudentClass.Gpalevel;
							studentClassDto.CertificationStatus = existingStudentClass.CertificationStatus;
							studentClassDto.CertificationDate = existingStudentClass.CertificationDate;
							studentClassDto.Method = existingStudentClass.Method;
						}

						await _elasticClient.UpdateAsync<StudentDTO>(hit.Id, u => u
							.Doc(studentDto)
							.RetryOnConflict(3)
						);
					}
				}
			}
		}
	}
}
