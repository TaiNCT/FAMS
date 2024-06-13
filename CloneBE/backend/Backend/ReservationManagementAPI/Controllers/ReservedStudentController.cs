using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using ReservationManagementAPI.Contracts;
using ReservationManagementAPI.Entities.DTOs;
using ReservationManagementAPI.Entities.Errors;
using Entities.Models;
using ReservationManagementAPI.Exceptions;
using ReservationManagementAPI.Repository;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Result = ReservationManagementAPI.Exceptions.Result;
using ClosedXML.Excel;
using System.Composition;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2013.Excel;
using System.Data;


namespace ReservationManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservedStudentController : ControllerBase
    {
        private readonly ILogger<ReservedStudentController> _logger;
        private readonly IElasticClient _client;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepo;
        private readonly IReservedClassRepository _reservedClassRepo;
        private readonly IClassRepository _classRepo;
        private readonly IExportRepository _exportRepository;

        public ReservedStudentController(ILogger<ReservedStudentController> logger,
            IElasticClient client, IMapper mapper,
            IStudentRepository studentRepo, IReservedClassRepository reservedClassRepo,
            IClassRepository classRepository, IExportRepository exportRepository)
        {
            _logger = logger;
            _client = client;
            _studentRepo = studentRepo;
            _reservedClassRepo = reservedClassRepo;
            _mapper = mapper;
            _classRepo = classRepository;
            _exportRepository = exportRepository;
        }

        [HttpPost(Name = "Create student reserved ElasticSearch")]
        public async Task<IActionResult> CreateClass([FromBody] List<StudentReservedDTO> models)
        {
            foreach (var model in models)
            {
                var result = await _client.IndexDocumentAsync(model);
            }
            return Ok();
        }


        [HttpGet("GetAllFromElastic/{rowPerPage}/{currentPage}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentReservedDTO>))]
        public async Task<IActionResult> GetAllStudents(int rowPerPage, int currentPage)
        {
        var results = await _client.SearchAsync<StudentReservedDTO>(s => s
            .Query(q => q.QueryString(d => d.Query("*")))
            .Sort(sort => sort.Descending(field => field.CreatedDate))
            .From((currentPage - 1) * rowPerPage)
            .Size(rowPerPage));

            var allResult = await _client.SearchAsync<StudentReservedDTO>(
            s => s.Query(
                  q => q.QueryString(
                      d => d.Query("*")
                      )
                  )
            .Size(10000));

            var countAllItem = allResult.Documents.ToList().Count();
             
            var pageCount = Math.Ceiling((decimal) countAllItem / rowPerPage);

            var response = new StudentReservedPagingResponseDTO()
            {
                StudentReservedList = results.Documents.ToList(),
                CurrentPage = currentPage,
                PageCount = (int)pageCount,
                ItemCount = countAllItem
                
            };

            return Ok(response);
        }


        [HttpGet ("SearchStudentsByElasticSearch/{keyword}/{rowPerPage}/{currentPage}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentReservedDTO>))]
        public async Task<IActionResult> GetStudent(string keyword, int rowPerPage, int currentPage)
        {
            var results = await _client.SearchAsync<StudentReservedDTO>(
                s => s.Query(
                    q => q.QueryString(
                        d => d.Query('*' + keyword + '*')
                        )
                    )
                .Sort(sort => sort.Descending(field => field.CreatedDate))
                .From((currentPage - 1) * rowPerPage)
                .Size(rowPerPage));

            var allResult = await _client.SearchAsync<StudentReservedDTO>(
            s => s.Query(
                  q => q.QueryString(
                      d => d.Query('*' + keyword + '*')
                      )
                  )
            .Size(10000));

            var countAllItem = allResult.Documents.ToList().Count();

            var pageCount = Math.Ceiling((decimal)countAllItem / rowPerPage);

            var response = new StudentReservedPagingResponseDTO()
            {
                StudentReservedList = results.Documents.ToList(),
                CurrentPage = currentPage,
                PageCount = (int)pageCount,
                ItemCount = countAllItem

            };

            return Ok(response);
        }
        [HttpGet("AdvancedSearch/{rowPerPage}/{currentPage}")]
        public async Task<IActionResult> AdvancedSearch([FromQuery] string[] keywords, int rowPerPage, int currentPage)
        {
            if (keywords.Length > 0)
            {
                if (keywords.Length == 1)
                {
                    var result = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*'))
                               )
                           )
                       ).Sort(sort => sort.Descending(field => field.CreatedDate))
                    .From((currentPage - 1) * rowPerPage)
                    .Size(rowPerPage));

                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*'))
                               )
                           )
                       )
                       .Size(10000));

                    var countAllItem = allResult.Documents.ToList().Count();

                    var pageCount = Math.Ceiling((decimal)countAllItem / rowPerPage);

                    var response = new StudentReservedPagingResponseDTO()
                    {
                        StudentReservedList = result.Documents.ToList(),
                        CurrentPage = currentPage,
                        PageCount = (int)pageCount,
                        ItemCount = countAllItem

                    };

                    return Ok(response);
                }

                if (keywords.Length == 2)
                {
                    var result = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                   m => m.QueryString(d => d.Query('*' + keywords[1] + '*'))
                               )
                           )
                       )
                       .Sort(sort => sort.Descending(field => field.CreatedDate))
                       .From((currentPage - 1) * rowPerPage)
                       .Size(rowPerPage));

                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                   m => m.QueryString(d => d.Query('*' + keywords[1] + '*'))
                               )
                           )
                       )
                       .Size(10000));

                    var countAllItem = allResult.Documents.ToList().Count();

                    var pageCount = Math.Ceiling((decimal)countAllItem / rowPerPage);

                    var response = new StudentReservedPagingResponseDTO()
                    {
                        StudentReservedList = result.Documents.ToList(),
                        CurrentPage = currentPage,
                        PageCount = (int)pageCount,
                        ItemCount = countAllItem

                    };

                    return Ok(response);
                }

                if (keywords.Length == 3)
                {
                    var result = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                   m => m.QueryString(d => d.Query('*' + keywords[1] + '*')),
                                    m => m.QueryString(d => d.Query('*' + keywords[2] + '*'))
                               )
                           )
                       )
                        .Sort(sort => sort.Descending(field => field.CreatedDate))
                        .From((currentPage - 1) * rowPerPage)
                        .Size(rowPerPage));

                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                      s => s.Query(q => q
                          .Bool(b => b
                              .Should(
                                  m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[1] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[2] + '*'))
                              )
                          )
                      )
                      .Size(10000));

                    var countAllItem = allResult.Documents.ToList().Count();

                    var pageCount = Math.Ceiling((decimal)countAllItem / rowPerPage);

                    var response = new StudentReservedPagingResponseDTO()
                    {
                        StudentReservedList = result.Documents.ToList(),
                        CurrentPage = currentPage,
                        PageCount = (int)pageCount,
                        ItemCount = countAllItem

                    };

                    return Ok(response);
                }

                if (keywords.Length == 4)
                {
                    var result = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                   m => m.QueryString(d => d.Query('*' + keywords[1] + '*')),
                                    m => m.QueryString(d => d.Query('*' + keywords[2] + '*')),
                                    m => m.QueryString(d => d.Query('*' + keywords[3] + '*'))
                               )
                           )
                       ).Sort(sort => sort.Descending(field => field.CreatedDate))
                    .From((currentPage - 1) * rowPerPage)
                    .Size(rowPerPage));

                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                      s => s.Query(q => q
                          .Bool(b => b
                              .Should(
                                  m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[1] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[2] + '*')),
                                   m => m.QueryString(d => d.Query('*' + keywords[3] + '*'))
                              )
                          )
                      )
                      .Size(10000));

                    var countAllItem = allResult.Documents.ToList().Count();

                    var pageCount = Math.Ceiling((decimal)countAllItem / rowPerPage);

                    var response = new StudentReservedPagingResponseDTO()
                    {
                        StudentReservedList = result.Documents.ToList(),
                        CurrentPage = currentPage,
                        PageCount = (int)pageCount,
                        ItemCount = countAllItem

                    };

                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpGet("GetAllReservedStudent")]
        public async Task<IActionResult> GetAllReservedStudents(int pageNumber)
        {
            var students = await _studentRepo.GetAllReservedStudents(pageNumber);

            return Ok(students);
        }

        [HttpGet("GetAllReservedStudent/ReverseTime")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentReservedDTO>))]
        public async Task<IActionResult> GetAllReservedStudentsByReverseTime()
        {
            var students = await _studentRepo.GetAllReservedStudentsByReserveTime();

            return Ok(students);
        }

        [HttpPut("UpdateDropOut")]
        public async Task<IActionResult> UpdateStatusStudents(string studentId, string reservedClassId)
        {
            Result result = await _studentRepo.UpdateStatusStudent(studentId, reservedClassId);
                var query = new MatchQuery
                {
                    Field = Infer.Field<StudentReservedDTO>(f => f.ReservedClassId),
                    Query = reservedClassId,
                };
                var resultDelete = await _client.DeleteByQueryAsync<StudentReservedDTO>(p => p.Index("reservedstudentlist2").Query(q => query));
            return Ok(result);
        }

        [HttpPut("UpdateReserveStatus")]
        public async Task<IActionResult> UpdateReserveStatusStudent(string studentId, string reservedClassId)
        {
            Result result = await _studentRepo.UpdateReserveStatusStudent(studentId, reservedClassId);

            //Delete reserved student in ElasticSearch DB
                var query = new MatchQuery
                {
                    Field = Infer.Field<StudentReservedDTO>(f => f.ReservedClassId),
                    Query = reservedClassId,
                };
            var resultDelete = await _client.DeleteByQueryAsync<StudentReservedDTO>(p => p.Index("reservedstudentlist2").Query(q => query));
            return Ok(result);
        }

        [HttpGet("SearchStudent")]
        public async Task<IActionResult> SearchStudent(string studentIdORemail)
        {
            var students = await _studentRepo.SearchStudent(studentIdORemail);
            return Ok(students);
        }

        [HttpPost("InsertStudent")]
        public async Task<IActionResult> InsertRseservedClass(string studentId, string classId, string reason, DateTime startDate, DateTime endDate)
        {
            var result = await _reservedClassRepo.InsertReservedClass(studentId,classId, reason, startDate, endDate);
            if (result != null)
            {
                
                    //Add student into Database of ElasticSearch
                    var objClass = await _classRepo.GetClassByClassId(classId);
                    StudentReservedDTO studentReservedDTO = await _studentRepo.GetReservedStudentDTOByStudentIdAndClassId(studentId, classId, reason, startDate, endDate, objClass.EndDate);
                    await _client.IndexDocumentAsync(studentReservedDTO);

                    return Ok(result);
                
            }
            else
            {
                return BadRequest("Invalid data!!!");
            }
        }

        /*[HttpGet("ValidateStudent")]
        public async Task<IActionResult> ValidateStudent(string studentId, string classId, string reason, DateTime startDate, DateTime endDate)
        {
            bool result = await _reservedClassRepo.validateInsertReserveStudent(studentId, classId, reason, startDate, endDate);                         

            return Ok(result);
           
        }*/

        [HttpGet("{studentId}", Name = "GetStudentById")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentReservedDTO>))]
        public async Task<IActionResult> GetStudentById(string studentId, string reservedClassId)
        {
            try
            {
                // Fetch student information from your data source (Elasticsearch)
                var result = await _studentRepo.GetStudentById(studentId);

                if (result == null)
                {
                    // If student with given ID is not found, return 404 Not Found
                    return NotFound("There is no student with this ID.");
                }

                // If student is found, return 200 OK with student information
                return Ok(result);
               
            }
            catch (Exception ex)
            {
                // If any error occurs during processing, return 500 Internal Server Error
                _logger.LogError(ex, "An error occurred while fetching student information by ID.");
                return StatusCode(500, "An error occurred while fetching student information.");
            }
        }
        [HttpPost("ExportReservedStudent")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentReservedDTO>))]
        public async Task<IActionResult> ExportReservedStudent([FromBody] List<StudentReservedDTO> models)
        {
            var table = await _exportRepository.exportReservedStudent(models);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(table, "ReservedList");
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Position = 0;
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reservedList.xlsx", true);
                }
            }
        }

        [HttpGet("GetAllFromElasticForExport")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentReservedDTO>))]
        public async Task<IActionResult> GetAllStudentsForExport()
        {
            try
            {
                var allResult = await _client.SearchAsync<StudentReservedDTO>(
                    s => s.Query(
                        q => q.QueryString(
                            d => d.Query("*")
                            )
                        )
                    .Size(10000));
                var list = allResult.Documents.ToList();
                return Ok(list);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
           
        }

        [HttpGet("SearchStudentsByElasticSearchForExport/{keyword}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentReservedDTO>))]
        public async Task<IActionResult> GetStudentForExport(string keyword)
        {
            try
            {
                var allResult = await _client.SearchAsync<StudentReservedDTO>(
                    s => s.Query(
                        q => q.QueryString(
                            d => d.Query('*' + keyword + '*')
                       )
                   )
                    .Size(10000));
                var list = allResult.Documents.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("AdvancedSearchForExport")]
        public async Task<IActionResult> AdvancedSearchForExport([FromQuery] string[] keywords)
        {
            if (keywords.Length > 0)
            {
                if (keywords.Length == 1)
                {

                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*'))
                               )
                           )
                       )
                       .Size(10000));

                    return Ok(allResult.Documents.ToList());
                }

                if (keywords.Length == 2)
                {
                    
                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                       s => s.Query(q => q
                           .Bool(b => b
                               .Should(
                                   m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                   m => m.QueryString(d => d.Query('*' + keywords[1] + '*'))
                               )
                           )
                       )
                       .Size(10000));

                    return Ok(allResult.Documents.ToList());
                }

                if (keywords.Length == 3)
                {
                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                      s => s.Query(q => q
                          .Bool(b => b
                              .Should(
                                  m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[1] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[2] + '*'))
                              )
                          )
                      )
                      .Size(10000));

                    return Ok(allResult.Documents.ToList());
                }

                if (keywords.Length == 4)
                {
                    var allResult = await _client.SearchAsync<StudentReservedDTO>(
                      s => s.Query(q => q
                          .Bool(b => b
                              .Should(
                                  m => m.QueryString(d => d.Query('*' + keywords[0] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[1] + '*')),
                                  m => m.QueryString(d => d.Query('*' + keywords[2] + '*')),
                                   m => m.QueryString(d => d.Query('*' + keywords[3] + '*'))
                              )
                          )
                      )
                      .Size(10000));

                    return Ok(allResult.Documents.ToList());
                }
            }
            return BadRequest();
        }

    }

}
