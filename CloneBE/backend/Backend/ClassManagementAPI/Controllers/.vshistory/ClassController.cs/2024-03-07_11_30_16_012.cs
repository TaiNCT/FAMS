using AutoMapper;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Models;
using ClassManagementAPI.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using System.IO;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClassManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IElasticClient _client;
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassUserRepository _classUserRepository;

        public ClassController(ILogger<ClassController> logger, IElasticClient client, IMapper mapper, IClassRepository classRepository, ILocationRepository locationRepository, IUserRepository userRepository, IClassUserRepository classUserRepository)
        {
            _logger = logger;
            _client = client;
            _classRepository = classRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
            _classUserRepository = classUserRepository;
        }

        [HttpGet(Name = "GetClass")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> GetClass()
        {
            _logger.LogInformation("Success");
            var totalCount = await _classRepository.GetTotalCount();
            var items = await _classRepository.GetClasses();
            var response = new ResponseDto("Get List of Class Successfully", 200, true, new { totalCount, items });
            return Ok(response);
        }

        [HttpGet("{keyword}", Name = "Search Class")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]           

        public async Task<IActionResult> Search(string keyword)
        {
            _logger.LogInformation("Successfully");
            string queryString = keyword;
            if (!int.TryParse(keyword, out int number))
            {
                queryString = "*" + keyword + "*";
            }

            var results = await _client.SearchAsync<Class>(
                s => s.Query(
                    q => q.QueryString(
                        d => d.Query(queryString)
                        )
                    ).Size(1000)
                );
            var totalCount = results.Total;
            var items = results.Documents.ToList();
            var response = new ResponseDto("Search Class Successfully", 200, true, new { totalCount, items });
            return Ok(response);
        }

        [HttpGet("calendar", Name = "Search Class In Calendar")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> SearchClassInCalendar([FromQuery] SearchKeyListDto keysDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            if (keysDto.searchKeys.Count() == 0)
            {
                return BadRequest("Empty array");
            }

            var keywords = keysDto.searchKeys;

            var shouldQueries = keywords.SelectMany(keyword => new[] {
                                                                        new QueryContainerDescriptor<Class>()
                                                                            .Match(m => m
                                                                                .Field(f => f.ClassCode)
                                                                                .Query(keyword)
                                                                            ),
                                                                        new QueryContainerDescriptor<Class>()
                                                                            .Match(m => m
                                                                                .Field(f => f.ClassName)
                                                                                .Query(keyword)
                                                                            )
                                                                    });

            var results = await _client.SearchAsync<Class>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(shouldQueries.ToArray())
                    )
                )
            );


            var totalCount = results.Total;
            var items = results.Documents.ToList();
            var response = new ResponseDto("Search Class Successfully", 200, true, new { totalCount, items });
            return Ok(response);
        }



        [HttpPost(Name = "Create class")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateClass([FromBody] ClassCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Fail: " + ModelState);
                var response = new ResponseDto("Invalid model state", 400, false, ModelState);
                return BadRequest(response);
            }

            try
            {
                _logger.LogInformation("Start Create");
                var classModel = _mapper.Map<Class>(model);
                var result = await _classRepository.CreateClass(classModel);
                if (result)
                {
                    await _client.IndexDocumentAsync(classModel);
                    _logger.LogInformation("Successful");
                    var response = new ResponseDto("Add Successfully", 200, true, result);
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("Fail to create");
                    var response = new ResponseDto("Something wrong while creating class!!!", 400, false);
                    return BadRequest(response);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError("Fail");
                var response = new ResponseDto(ex.Message, 400, false);
                return BadRequest(response);
            }
        }


        [HttpGet("Filter")]
        public async Task<IActionResult> GetClassesListByFilter([FromQuery] FilterFormatDto filterData)
        {
            try
            {

                var filterDataList = await _classRepository.GetClassListByFilter(filterData);

                var result = _mapper.Map<PagedResult<GetClassDto>>(filterDataList);

                var response = new ResponseDto("Operation successful", 200, true, result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting classes for Filter.");
                var response = new ResponseDto("Error retrieving data from the database", 500, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("ViewInfo")]
        public async Task<IActionResult> GetInfoClass([FromQuery] string classId, [FromQuery] string userType = null)
        {
            try
            {
                if (classId == null) return BadRequest(new ResponseDto("Operation fail", 400, false));
                if (userType == null) userType = string.Empty;
                var classNow = await _classRepository.GetClassById(classId);
                var location = await _locationRepository.GetLocationById(classNow.LocationId);
                var listUserId = await _classUserRepository.GetClassUsersByClassID(classId, userType);
                var listUser = await _userRepository.GetAllUserByListUserID(listUserId);

                var result = new ViewInfoClassDto
                {
                    Location = location.Name,
                    Users = listUser,
                };
                var response = new ResponseDto("Operation successful", 200, true, result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting classes for Filter.");
                var response = new ResponseDto("Error retrieving data from the database", 500, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet("ViewInfoClassDetail")]
        public async Task<IActionResult> GetInfoClass([FromQuery] string classId)
        {
            try
            {
                if (classId == null) return BadRequest(new ResponseDto("Operation fail", 400, false));
                var classNow = await _classRepository.GetClassById(classId);
                if (classNow != null)
                {
                    var result = new ViewInfoClassDetailDto
                    {
                        TotalHours = classNow.TotalHours,
                        TotalDays = classNow.TotalDays,
                        StartTime = classNow.StartTime,
                        EndTime = classNow.EndTime,
                        StartDate = classNow.StartDate,
                        EndDate = classNow.EndDate,
                        ClassCode = classNow.ClassCode,
                        ClassStatus = classNow.ClassStatus,
                        ClassName = classNow.ClassName,
                        ClassId = classNow.ClassId,
                        ReviewBy = classNow.ReviewBy,
                        ReviewDate = classNow.ReviewDate,
                        ApprovedBy = classNow.ApprovedBy,
                        ApprovedDate = classNow.ApprovedDate,
                        CreatedBy = classNow.CreatedBy,
                        CreatedDate = classNow.CreatedDate,
                        AcceptedAttendee = classNow.AcceptedAttendee,
                        ActualAttendee = classNow.ActualAttendee,
                        PlannedAttendee = classNow.PlannedAttendee,
                        AttendeeTypeName = classNow.AttendeeLevel?.AttendeeTypeName,
                        LocationName = classNow.Location?.Name,
                        
                        Fsu = new ViewInfoClassFsuDto
                        {
                            Name = classNow.Fsu.Name,
                            Email = classNow.Fsu.Email
                        },
                        TrainingProgram = new ViewInfoClassTrainingProgramDto
                        {
                            UpdatedBy = classNow.TrainingProgramCodeNavigation?.UpdatedBy,
                            UpdatedDate = classNow.TrainingProgramCodeNavigation?.UpdatedDate,
                            Days = classNow.TrainingProgramCodeNavigation?.Days,
                            Hours = classNow.TrainingProgramCodeNavigation?.Hours,
                            Name = classNow.TrainingProgramCodeNavigation?.Name
                        },
                        Syllabus = classNow.TrainingProgramCodeNavigation.TrainingProgramSyllabi.Select(syllabus => new ViewInfoClassSyllabusDto
                        {
                            SyllabusId = syllabus.SyllabusId,
                            TopicName = syllabus.Syllabus.TopicName,
                            Version = syllabus.Syllabus.Version,
                            TopicCode = syllabus.Syllabus.TopicCode,
                            Days = syllabus.Syllabus.Days,
                            Hours = syllabus.Syllabus.Hours,
                            ModifiedBy = syllabus.Syllabus.ModifiedBy,
                            ModifiedDate = syllabus.Syllabus.ModifiedDate,
                            Status = syllabus.Syllabus.Status
                        }).ToList()
                    };
                    var response = new ResponseDto("Operation successful", 200, true, result);
                    return Ok(response);
                }   
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting classes for Filter.");
                var response = new ResponseDto("Error retrieving data from the database", 500, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("Week")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> GetListClassesByDateRange(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var result = await _classRepository.GetListOfClassesByDateRange(startDate, endDate);

                if (result.TotalCount == 0)
                {
                    return NotFound();
                }

                var response = new ResponseDto("Get List of Class Successfully", 200, true, result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting classes by date range");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("Attendee/{id}")]
        public async Task<IActionResult> GetByAttendee(string id)
        {
            try
            {
                var attendee = await _classRepository.GetClassByAttendeeId(id);
                return Ok(attendee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting attendee by ID.");
                var response = new ResponseDto("Error retrieving data from the database", 500, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("Time")]
        public async Task<IActionResult> GetByTime([FromQuery] FilterTime? filterTime)
        {
            try
            {
                var classes = await _classRepository.GetClassByTime(filterTime);
                return Ok(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting class by time.");
                var response = new ResponseDto("Error retrieving data from the database", 500, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
