using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Entities.Context;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Nest;
using StudentInfoManagementAPI.DTO;
using System.Collections.Immutable;
using System.Data;
using System.Web;

namespace StudentInfoManagementAPI.Service
{
    public class StudentService_QuyNDC : IStudentService_QuyNDC
    {
        public readonly FamsContext _dbcontext;
        private readonly IElasticClient _elasticClient;
        private readonly ResponseDTO _responseDTO;

        public StudentService_QuyNDC(FamsContext dbcontext, IElasticClient elasticClient)
        {
            _dbcontext = dbcontext;
            _responseDTO = new ResponseDTO();
            _elasticClient = elasticClient;
        }

        public async Task<ResponseDTO> ChangeStudentStatus(string id)
        {
            try
            {
                var mustQueries = new List<Func<QueryContainerDescriptor<StudentDTOO>, QueryContainer>>();

                if (!string.IsNullOrEmpty(id))
                {
                    // Add match query for attending status
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.StudentId)
                            .Query(id) // Use the status parameter here
                        ));
                }

                var searchResponse = _elasticClient.Search<StudentDTOO>(s => s
                        .From(0)
                        .Size(10000)
                        .Query(q => q
                            .Bool(b => b
                                .Must(mustQueries.ToList()))));

                if (searchResponse.Hits.Count > 0)
                {
                    var student = searchResponse.Documents.FirstOrDefault();
                    var elasticId = searchResponse.Hits.FirstOrDefault().Id;
                    var studentToUpdate = _dbcontext.Students.FirstOrDefault(s => s.StudentId == id);
                    if (student != null && student.StudentInfoDTO.Status.ToLower() == "inactive")
                    {
                        student.StudentInfoDTO.Status = "Disabled";
                        studentToUpdate.Status = "Disabled";
                        _dbcontext.SaveChanges();
                        var updateResponse = await _elasticClient.UpdateAsync<StudentDTOO>(elasticId, u => u
                        .Doc(student)
                        .RetryOnConflict(3));

                        if (!updateResponse.IsValid)
                        {
                            _responseDTO.Result = student.StudentInfoDTO.Status;
                            _responseDTO.IsSuccess = false;
                            _responseDTO.Message = $"Failed to update student status: {updateResponse.ServerError}";
                            return _responseDTO;
                        }
                        else
                        {
                            _responseDTO.Result = student;
                            _responseDTO.Message = $"Update student status successful, new status: {student.StudentInfoDTO.Status}";
                            _responseDTO.IsSuccess = true;
                            return _responseDTO;
                        }
                    }
                    _responseDTO.Result = student;
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = $"Cannot update student status for the status is not Inactive";
                }
                else
                {
                    _responseDTO.Result = null;
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "No student found";
                    return _responseDTO;
                }

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetListStudent(string? keyword, string? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc", int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var mustQueries = new List<Func<QueryContainerDescriptor<StudentDTO>, QueryContainer>>();

                if (!string.IsNullOrEmpty(status))
                {
                    // Add match query for attending status
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Status)
                            .Query(status) // Use the status parameter here
                        ));
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Gender)
                            .Query(gender)
                        ));
                }


                if (!string.IsNullOrEmpty(dob))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Dob)
                            .Query(dob)
                        ));
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                     .QueryString(qs => qs
                         .Query("*" + keyword + "*")
                         .Fields(f => f
                             .Field(ff => ff.StudentInfoDTO.FullName)
                             .Field(ff => ff.StudentInfoDTO.Address)
                             .Field(ff => ff.StudentInfoDTO.Email)
                             .Field(ff => ff.MajorDTO.Name)
                         )
                     ));
                }

                var searchResponse = _elasticClient.Search<StudentDTO>(s => s
                        .From(0)
                        .Size(10000)
                        .Query(q => q
                            .Bool(b => b
                                .Must(mustQueries.ToList()))));

                if (searchResponse.Hits.Count > 0)
                {
                    var filteredStudents = searchResponse.Documents.ToList();
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortedStudents = sortStudent(filteredStudents, sortBy, sortOrder);
                        var result = sortedStudents.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                        var paginated = new PaginatedStudentListDTO
                        {
                            Students = result,
                            TotalCount = sortedStudents.Count(),
                        };
                        _responseDTO.Result = paginated;
                    }
                    else
                    {
                        var result = filteredStudents.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                        var paginated = new PaginatedStudentListDTO
                        {
                            Students = result,
                            TotalCount = filteredStudents.Count(),
                        };
                        _responseDTO.Result = paginated;
                    }


                }
                else
                {
                    _responseDTO.Result = null;
                    return _responseDTO;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }
        public DataTable exportStudentInSystem(string? keyword, string? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc")
        {
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("Date of birth", typeof(DateTime));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("University", typeof(string));
            dt.Columns.Add("Major", typeof(string));
            dt.Columns.Add("Graduate Time", typeof(DateTime));
            dt.Columns.Add("GPA", typeof(decimal));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("RECer", typeof(string));
            dt.Columns.Add("Status", typeof(string));

            try
            {
                var mustQueries = new List<Func<QueryContainerDescriptor<StudentDTO>, QueryContainer>>();

                if (!string.IsNullOrEmpty(status))
                {
                    // Add match query for attending status
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Status)
                            .Query(status) // Use the status parameter here
                        ));
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Gender)
                            .Query(gender)
                        ));
                }

                if (!string.IsNullOrEmpty(dob))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Dob)
                            .Query(dob)
                        ));
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                     .QueryString(qs => qs
                         .Query("*" + keyword + "*")
                         .Fields(f => f
                             .Field(ff => ff.StudentInfoDTO.FullName)
                             .Field(ff => ff.StudentInfoDTO.Address)
                             .Field(ff => ff.StudentInfoDTO.Email)
                             .Field(ff => ff.MajorDTO.Name)

                         )
                     ));
                }

                // Add query string query for full name and faaccount

                var searchResponse = _elasticClient.Search<StudentDTO>(s => s
                        .From(0)
                        .Size(10000)
                        .Query(q => q
                            .Bool(b => b
                                .Must(mustQueries.ToList()))
                )
                    );

                if (searchResponse.Hits.Count > 0)
                {
                    var filledListElastic = searchResponse.Documents.ToList();
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortedStudents = sortStudent(filledListElastic, sortBy, sortOrder).ToList();
                        sortedStudents.ForEach(item =>
                        {
                            dt.Rows.Add(item.StudentInfoDTO.FullName, item.StudentInfoDTO.Dob, item.StudentInfoDTO.Gender, item.StudentInfoDTO.Phone, item.StudentInfoDTO.Email, item.StudentInfoDTO.University, item.MajorDTO.Name, item.StudentInfoDTO.GraduatedDate, item.StudentInfoDTO.Gpa, item.StudentInfoDTO.Address, item.StudentInfoDTO.Recer, item.StudentInfoDTO.Status);
                        });
                    }
                    else
                    {
                        filledListElastic.ForEach(item =>
                        {
                            dt.Rows.Add(item.StudentInfoDTO.FullName, item.StudentInfoDTO.Dob, item.StudentInfoDTO.Gender, item.StudentInfoDTO.Phone, item.StudentInfoDTO.Email, item.StudentInfoDTO.University, item.MajorDTO.Name, item.StudentInfoDTO.GraduatedDate, item.StudentInfoDTO.Gpa, item.StudentInfoDTO.Address, item.StudentInfoDTO.Recer, item.StudentInfoDTO.Status);
                        });
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return dt;

        }

        private IEnumerable<StudentDTO> sortStudent(IEnumerable<StudentDTO> students, string sortBy, string sortOrder)
        {
            switch (sortBy.ToLower())
            {
                case "fullname":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.FullName) : students.OrderByDescending(s => s.StudentInfoDTO.FullName);
                    break;
                case "dateofbirth":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Dob) : students.OrderByDescending(s => s.StudentInfoDTO.Dob);
                    break;
                case "gender":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Gender) : students.OrderByDescending(s => s.StudentInfoDTO.Gender);
                    break;
                case "phone":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Phone) : students.OrderByDescending(s => s.StudentInfoDTO.Phone);
                    break;
                case "email":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Email) : students.OrderByDescending(s => s.StudentInfoDTO.Email);
                    break;
                case "university":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.University) : students.OrderByDescending(s => s.StudentInfoDTO.University);
                    break;
                case "major":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.MajorDTO.Name) : students.OrderByDescending(s => s.MajorDTO.Name);
                    break;
                case "graduationtime":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.GraduatedDate) : students.OrderByDescending(s => s.StudentInfoDTO.GraduatedDate);
                    break;
                case "gpa":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Gpa) : students.OrderByDescending(s => s.StudentInfoDTO.Gpa);
                    break;
                case "address":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Address) : students.OrderByDescending(s => s.StudentInfoDTO.Address);
                    break;
                case "recer":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Recer) : students.OrderByDescending(s => s.StudentInfoDTO.Recer);
                    break;
                case "status":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Status) : students.OrderByDescending(s => s.StudentInfoDTO.Status);
                    break;
                case "id":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.Id) : students.OrderByDescending(s => s.StudentInfoDTO.Id);
                    break;

            }
            return students;
        }

        public ResponseDTO GetMajor(string id)
        {
            try
            {
                var major = _dbcontext.Majors.Where(s => s.MajorId == id);
                _responseDTO.Result = major;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetAllId(string? keyword, string? dob, string? gender, string? status, string? sortBy, string? sortOrder, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var mustQueries = new List<Func<QueryContainerDescriptor<StudentDTO>, QueryContainer>>();

                if (!string.IsNullOrEmpty(status))
                {
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Status)
                            .Query(status) // Use the status parameter here
                        ));
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Gender)
                            .Query(gender)
                        ));
                }


                if (!string.IsNullOrEmpty(dob))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Dob)
                            .Query(dob)
                        ));
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Add match query for gender
                    mustQueries.Add(mq => mq
                     .QueryString(qs => qs
                         .Query("*" + keyword + "*")
                         .Fields(f => f
                             .Field(ff => ff.StudentInfoDTO.FullName)
                             .Field(ff => ff.StudentInfoDTO.Address)
                             .Field(ff => ff.StudentInfoDTO.Email)
                         )
                     ));
                }

                var searchResponse = _elasticClient.Search<StudentDTO>(s => s
                        .From(0)
                        .Size(200)
                        .Query(q => q
                            .Bool(b => b
                                .Must(mustQueries.ToList()))));

                if (searchResponse.Hits.Count > 0)
                {
                    var filteredStudents = searchResponse.Documents.ToList();
                    var studentIdList = new List<string>();
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortedStudents = sortStudent(filteredStudents, sortBy, sortOrder);
                        foreach (StudentDTO student in sortedStudents)
                        {
                            studentIdList.Add(student.StudentInfoDTO.StudentId);
                        }
                    }
                    else
                    {
                        foreach (StudentDTO student in filteredStudents)
                        {
                            studentIdList.Add(student.StudentInfoDTO.StudentId);
                        }
                    }
                    _responseDTO.Result = studentIdList;
                    _responseDTO.Message = studentIdList.Count.ToString();
                }
                else
                {
                    _responseDTO.Result = null;
                    return _responseDTO;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        public ResponseDTO GetAllMajor()
        {
            var listMajor = _dbcontext.Majors;
            _responseDTO.Result = listMajor;
            return _responseDTO;
        }

        public ResponseDTO GetAllClass()
        {
            var listClass = _dbcontext.Classes;
            _responseDTO.Result = listClass;
            return _responseDTO;
        }
    }

}
