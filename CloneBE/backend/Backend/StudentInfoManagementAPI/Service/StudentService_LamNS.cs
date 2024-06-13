using AutoMapper;
using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using StudentInfoManagementAPI.DTO;
using System.Data;

namespace StudentInfoManagementAPI.Service
{
    public class StudentService_LamNS : IStudentService_LamNS
    {
        public readonly FamsContext _dbcontext;
        private IMapper _mapper;
        private ResponseDTO _responseDTO;
        private readonly IElasticClient _elasticClient;


        public StudentService_LamNS(FamsContext dbcontext, IMapper mapper, IElasticClient elasticClient)
        {
            _dbcontext = dbcontext;
            _responseDTO = new ResponseDTO();
            _mapper = mapper;
            _elasticClient = elasticClient;
        }

        public async Task<bool> DeleteData()
        {
            //Check is index exist
            var isExists = await _elasticClient.Indices.ExistsAsync("students");
            if (isExists.Exists)
            {
                //Delete index
                var response = _elasticClient.Indices.Delete("students");
                return response.IsValid;
            }
            return true;
            
        }

        public async Task<IEnumerable<StudentDTO>> getAllDocument()
        {
            var searchResponse = await _elasticClient.SearchAsync<StudentDTO>(s => s.MatchAll().Size(1000));
            return searchResponse.Documents;
        }

        public async Task<ResponseDTO> GetListStudentInClass(string classId, string? keyword, DateTime? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc", int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                //Create list query for elastic
                var mustQueries = new List<Func<QueryContainerDescriptor<StudentDTO>, QueryContainer>>();


                //Add query of class id
                mustQueries.Add(mq => mq
                       .Match(m => m
                           .Field(f => f.StudentClassDTOs.First().ClassId.Suffix("keyword"))
                           .Query(classId) 
                       ));

                // Add query for attending status
                if (!string.IsNullOrEmpty(status))
                {
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentClassDTOs.First().AttendingStatus)
                            .Query(status) 
                        ));
                }

                // Add query for attending gender

                if (!string.IsNullOrEmpty(gender))
                {
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Gender)
                            .Query(gender)
                        ));
                }


                // Add search query multiple fields search
                if (!string.IsNullOrEmpty(keyword))
                {
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

                // Get list docs match

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
                    //Filter out by dob
                    if (dob != null)
                    {
                        filledListElastic = filledListElastic.Where(s => s.StudentInfoDTO.Dob.ToShortDateString() == dob.Value.ToShortDateString()).ToList();
                    }

                    //Filter out by status and specific class id
                    if (!string.IsNullOrEmpty(status))
                    {
                        string[] desiredStatuses = status.Split(',');

                        // Filter the StudentDTO objects based on the desired statuses
                        filledListElastic = filledListElastic
                            .Where(s => s.StudentClassDTOs.Any(c => desiredStatuses.Contains(c.AttendingStatus) && c.ClassId == classId))
                            .ToList();
                    }

                    //Get only what student class have specific class id
                    filledListElastic = filledListElastic.Select(s =>
                    {
                        s.StudentClassDTOs = s.StudentClassDTOs.Where(c => c.ClassId == classId).ToList();
                        return s;
                    }).ToList();

                    //Call function sort student
                    var sortedStudents = sortStudent(filledListElastic, sortBy, sortOrder);


                    //Get number of result base on page number and page size

                    var result = sortedStudents.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    var paginated = new PaginatedStudentListDTO
                    {
                        Students = result,
                        TotalCount = filledListElastic.Count(),
                    };
                    _responseDTO.Result = paginated;
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

        public ResponseDTO getListClassStudentNotIn(string studentId)
        {
            try
            {
                //Get current time to check start and end time of class
                var today = DateOnly.FromDateTime(DateTime.Today);

                //Get list classes that student not in and available
                var listClass = _dbcontext.Classes
                    .Include(c => c.StudentClasses)
                    .Where(c => !c.StudentClasses.Any(sc => sc.StudentId == studentId) && c.StartDate < today && c.EndDate > today && c.ClassStatus =="In class")
                    .ToList();
                _responseDTO.Result = _mapper.Map<IEnumerable<ClassDTO>>(listClass);
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> addNewClasInfor(StudentClassDTO studentClassDTO)
        {
            try
            {
                var studentClass = _mapper.Map<StudentClass>(studentClassDTO);
                _dbcontext.StudentClasses.Add(studentClass);

                await _dbcontext.SaveChangesAsync();

                //Get updated status student (if have) to update to elastic
                Student nStudent = _dbcontext.Students.FirstOrDefault(s => s.StudentId == studentClassDTO.StudentId);
                await UpdateStudentClassInforElastic(studentClass, nStudent);
                _responseDTO.Result = "Add Success";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<bool> UpdateStudentClassInforElastic(StudentClass studentClass, Student student)
        {
            try
            {
                //Query of client to add new student class and update student infor of document selected by student id
                var response = await _elasticClient.UpdateByQueryAsync<StudentDTO>(u => u
                                    .Script(s => s
                                        .Source(@"
                                            if (ctx._source.studentInfoDTO.studentId == params.studentId) {
                                                ctx._source.studentClassDTOs.add(params.newStudentClassObject);
                                                ctx._source.studentInfoDTO = params.newStudentInfoDTO;
                                            }
                                        ")
                                        .Lang("painless")
                                        //add values for params
                                        .Params(p => p.Add("studentId", studentClass.StudentId) 
                                                      .Add("newStudentClassObject", _mapper.Map<StudentClassDTO>(studentClass))
                                                      .Add("newStudentInfoDTO", _mapper.Map<StudentInfoDTO>(student)))
                                    )
                                    .Query(q => q
                                        .Match(m => m
                                            .Field(f => f.StudentInfoDTO.StudentId)
                                            .Query(studentClass.StudentId)
                                        )
                                    )
                                );
                return response.IsValid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable exportStudentInclass(string classId, string? keyword, DateTime? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc")
        {
            //Create data table to push data want to export
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("Birth Day", typeof(DateTime));
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
                //Create list query for elastic
                var mustQueries = new List<Func<QueryContainerDescriptor<StudentDTO>, QueryContainer>>();


                //Add query class id
                mustQueries.Add(mq => mq
                       .Match(m => m
                           .Field(f => f.StudentClassDTOs.First().ClassId.Suffix("keyword"))
                           .Query(classId)
                       ));

                //Add query attending status
                if (!string.IsNullOrEmpty(status))
                {
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentClassDTOs.First().AttendingStatus)
                            .Query(status) 
                        ));
                }

                // Add query for gender
                if (!string.IsNullOrEmpty(gender))
                {
                    mustQueries.Add(mq => mq
                        .Match(m => m
                            .Field(f => f.StudentInfoDTO.Gender)
                            .Query(gender)
                        ));
                }

                //Search query multiple match
                if (!string.IsNullOrEmpty(keyword))
                {
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

                // add list query to elastic 

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
                    //Filter out dob
                    if (dob != null)
                    {
                        filledListElastic = filledListElastic.Where(s => s.StudentInfoDTO.Dob.ToShortDateString() == dob.Value.ToShortDateString()).ToList();
                    }
                    if (!string.IsNullOrEmpty(status))
                    {
                        string[] desiredStatuses = status.Split(',');

                        // Filter the StudentDTO objects based on the desired statuses
                        filledListElastic = filledListElastic
                            .Where(s => s.StudentClassDTOs.Any(c => desiredStatuses.Contains(c.AttendingStatus) && c.ClassId == classId))
                            .ToList();
                    }

                    //Filter out student class having exactly class id
                    filledListElastic = filledListElastic.Select(s =>
                    {
                        s.StudentClassDTOs = s.StudentClassDTOs.Where(c => c.ClassId == classId).ToList();
                        return s;
                    }).ToList();

                    //Call function sort
                    var sortedStudents = sortStudent(filledListElastic, sortBy, sortOrder).ToList();

                    //Add data to table
                    sortedStudents.ForEach(item =>
                    {
                        dt.Rows.Add(item.StudentInfoDTO.FullName, item.StudentInfoDTO.Dob, item.StudentInfoDTO.Gender, item.StudentInfoDTO.Phone, item.StudentInfoDTO.Email, item.StudentInfoDTO.University, item.MajorDTO.Name, item.StudentInfoDTO.GraduatedDate, item.StudentInfoDTO.Gpa, item.StudentInfoDTO.Address, item.StudentInfoDTO.Recer, item.StudentClassDTOs.First().AttendingStatus);
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return dt;

        }

        public ResponseDTO getListClassInfor(string studentId)
        {
            try
            {
                //Get list student class of student
                var ListClass = _dbcontext.StudentClasses.Include(sc => sc.Class).Where(sc => sc.StudentId == studentId).OrderByDescending(sc => sc.Id);
                _responseDTO.Result = _mapper.Map<IEnumerable<StudentClassDTO>>(ListClass);
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        //Function to sync data from db to elastic
        public async Task<string> CreateDocumentAsync()
        {
            try
            {
                //Delete index before sync data
                var result = await DeleteData();
                if (result)
                {
                    //Get all student
                    var Document = _dbcontext.Students.Include(s => s.StudentClasses).Include(s => s.Major).ToList();
                //Convert to studentDTO for elastic
                var studentDTOs = Document.Select(student => new StudentDTO
                {
                    StudentInfoDTO = _mapper.Map<StudentInfoDTO>(student),
                    MajorDTO = _mapper.Map<MajorDTO>(student.Major),
                    StudentClassDTOs = _mapper.Map<IEnumerable<StudentClassDTO>>(student.StudentClasses.ToList())
                }).ToList();

                foreach (var student in studentDTOs)
                {
                    var response = await _elasticClient.IndexDocumentAsync(student);
                    if (!response.IsValid)
                    {
                        return $"Failed to index user {student.StudentInfoDTO.StudentId}: {response.ServerError.Error.Reason}";
                    }

                }

                return "Document created successfully";
                }
                else
                {
                    return "Failed to delete old data";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Function for select all
        public ResponseDTO SelectAllStudentByClass(string classId)
        {
            try
            {
                var listStudent = _dbcontext.Students.Include(s => s.StudentClasses.Where(sc => sc.ClassId == classId)).Where(s => s.StudentClasses.Any(sc => sc.ClassId == classId)).Select(s => s.StudentId).ToList();
                _responseDTO.Result = listStudent;
            }
            catch (Exception e)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = e.Message;
            }
            return _responseDTO;
        }

        //Function to sort students
        private IEnumerable<StudentDTO> sortStudent(IEnumerable<StudentDTO> students, string sortBy, string sortOrder)
        {
            switch (sortBy.ToLower())
            {
                case "fullname":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentInfoDTO.FullName) : students.OrderByDescending(s => s.StudentInfoDTO.FullName);
                    break;
                case "birthday":
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
                case "graduatetime":
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
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentClassDTOs.First().AttendingStatus) : students.OrderByDescending(s => s.StudentClassDTOs.First().AttendingStatus);
                    break;
                case "id":
                    students = sortOrder == "asc" ? students.OrderBy(s => s.StudentClassDTOs.First().Id) : students.OrderByDescending(s => s.StudentClassDTOs.First().Id);
                    break;
            }
            return students;
        }

        public async Task<ResponseDTO> testUpdateStudent(UpdateStudentDTO newStudent)
        {
            var searchResponse = _elasticClient.Search<StudentDTO>(s => s
                .Query(q => q
                    .MatchPhrase(t => t
                        .Field(f => f.StudentInfoDTO.StudentId)
                        .Query(newStudent.studentid)
                    )
                )
            );

            if (searchResponse.Hits.Count == 0)
            {
                _responseDTO.Result = "Student not found";
                return _responseDTO;
            }

            var studentDocument = searchResponse.Hits.First().Source;
            studentDocument.StudentInfoDTO.FullName = newStudent.name.Trim();
            studentDocument.StudentInfoDTO.Gender = newStudent.gender.Trim();
            studentDocument.StudentInfoDTO.Dob = newStudent.dob;


            studentDocument.StudentInfoDTO.Phone = newStudent.phone.Trim();
            studentDocument.StudentInfoDTO.Email = newStudent.email.Trim();
            studentDocument.StudentInfoDTO.Address = newStudent.location.Trim();

            var studentClass = studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == newStudent.classid);

            if (studentClass != null){
                studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == newStudent.classid).AttendingStatus = newStudent.status.Trim();
                studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == newStudent.classid).CertificationStatus = newStudent.certificateStatus ? 1 : 0;
                studentDocument.StudentClassDTOs.FirstOrDefault(sc => sc.ClassId == newStudent.classid).CertificationDate = newStudent.certificateDate;
            }
            else
            {
                _responseDTO.Result = "Student class not found";
                return _responseDTO;
            }

            var updateResponse = await _elasticClient.UpdateAsync<StudentDTO>(searchResponse.Hits.First().Id, u => u
                .Doc(studentDocument)
                .RetryOnConflict(3)
            );

            if (updateResponse.IsValid)
            {
                _responseDTO.Result = "Student updated successfully";
            }
            else
{
    _responseDTO.Result = "Failed to update student";
}

return _responseDTO;
        }

       
    }
}
