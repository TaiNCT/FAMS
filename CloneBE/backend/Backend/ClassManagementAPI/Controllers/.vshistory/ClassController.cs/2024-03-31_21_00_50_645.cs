using System.IO;
using AutoMapper;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Dto.FilterDTO;
using ClassManagementAPI.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using System.IO;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Http.HttpResults;
using ClassManagementAPI.Dto.FilterDTO;
using Microsoft.AspNetCore.Authorization;
using ClassManagementAPI.Dto.SyllabusDTO;
using System.Net;
using ClassManagementAPI.Dto.UserDTO;

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
        private readonly ISyllabusRepository _syllabusRepository;

        public ClassController(ILogger<ClassController> logger, IElasticClient client, IMapper mapper, IClassRepository classRepository, ILocationRepository locationRepository, IUserRepository userRepository, IClassUserRepository classUserRepository, ISyllabusRepository syllabusRepository)

        {
            _logger = logger;
            _client = client;
            _classRepository = classRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
            _classUserRepository = classUserRepository;
            _syllabusRepository = syllabusRepository;
        }

        [HttpGet(Name = "GetClass")]
        //[Authorize(Roles = "Staff")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> GetClass()
        {
            _logger.LogInformation("Success");
            var totalCount = await _classRepository.GetTotalCount();
            var items = await _classRepository.GetClasses();
            var response = new ResponseDto(
                "Get List of Class Successfully",
                200,
                true,
                new { totalCount, items }
            );
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

            var results = await _client.SearchAsync<Class>(s =>
                s.Query(q => q.QueryString(d => d.Query(queryString))).Size(1000)
            );
            var totalCount = results.Total;
            var items = results.Documents.ToList();
            var response = new ResponseDto(
                "Search Class Successfully",
                200,
                true,
                new { totalCount, items }
            );
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

            var shouldQueries = keywords.SelectMany(keyword =>
                new[]
                {
                    new QueryContainerDescriptor<Class>().Match(m =>
                        m.Field(f => f.ClassCode).Query(keyword)
                    ),
                    new QueryContainerDescriptor<Class>().Match(m =>
                        m.Field(f => f.ClassName).Query(keyword)
                    )
                }
            );

            var results = await _client.SearchAsync<Class>(s =>
                s.Query(q => q.Bool(b => b.Should(shouldQueries.ToArray())))
            );

            var totalCount = results.Total;
            var items = results.Documents.ToList();
            var response = new ResponseDto(
                "Search Class Successfully",
                200,
                true,
                new { totalCount, items }
            );
            return Ok(response);
        }

        [HttpPost(Name = "Create class")]
        [ProducesResponseType(201, Type = typeof(ResponseDto))]
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
                    var response = new ResponseDto("Add Successfully", 200, true, classModel);
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("Fail to create");
                    var response = new ResponseDto(
                        "Something wrong while creating class!!!",
                        400,
                        false
                    );
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
        public async Task<IActionResult> GetClassesListByFilter(
            [FromQuery] FilterFormatDto filterData
        )
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
                var response = new ResponseDto(
                    "Error retrieving data from the database",
                    500,
                    false,
                    ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("FilterWeek")]
        public async Task<IActionResult> GetClassesListInAWeekByFilter(
            [FromQuery] FilterFormatDto filterData
        )
        {
            try
            {
                var filterDataList = await _classRepository.GetClassListInWeekByFilter(filterData);

                var result = _mapper.Map<PagedResult<WeekResultDto>>(filterDataList);

                var response = new ResponseDto("Operation successful", 200, true, result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting classes for Filter.");
                var response = new ResponseDto(
                    "Error retrieving data from the database",
                    500,
                    false,
                    ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("ViewInfo")]
        public async Task<IActionResult> GetInfoClass(
            [FromQuery] string classId,
            [FromQuery] string userType = null
        )
        {
            try
            {
                if (classId == null)
                    return BadRequest(new ResponseDto("Operation fail", 400, false));
                if (userType == null)
                    userType = string.Empty;
                var classNow = await _classRepository.GetClassById(classId);
                var location = await _locationRepository.GetLocationById(classNow.LocationId);
                var listUserId = await _classUserRepository.GetClassUsersByClassID(
                    classId,
                    userType
                );
                var listUser = await _userRepository.GetAllUserByListUserID(listUserId);

                var result = new ViewInfoClassDto { Location = location.Name, Users = listUser, };
                var response = new ResponseDto("Operation successful", 200, true, result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting classes for Filter.");
                var response = new ResponseDto(
                    "Error retrieving data from the database",
                    500,
                    false,
                    ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("ViewInfoClassDetail")]
        public async Task<IActionResult> GetInfoClass([FromQuery] string classId)
        {
            try
            {
                if (classId == null)
                    return BadRequest(new ResponseDto("Operation fail", 400, false));
                var classNow = await _classRepository.GetClassById(classId);
                List<ViewInfoClassSyllabusDto> Syllabus = null;
                if (classNow.TrainingProgramCodeNavigation == null)
                {
                    Syllabus = null;
                }
                else
                {
                    Syllabus = classNow
                       .TrainingProgramCodeNavigation.TrainingProgramSyllabi.Select(
                           syllabus => new ViewInfoClassSyllabusDto
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
                           }
                       )
                       .ToList();
                }
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
                        Users = classNow
                            .ClassUsers?.Select(user => new ViewInfoClassUserDto
                            {
                                UserId =user.User.UserId,
                                Id = user.User.Id,
                                FullName = user.User.FullName,
                                Email = user.User.Email,
                                Phone = user.User.Phone,
                                RoleId = user.User.RoleId,
                                RoleName = user.User.Role.RoleName
                            })
                            .ToList(),
                        Fsu = new ViewInfoClassFsuDto
                        {
                            FsuId = classNow.Fsu.FsuId,
                            Name = classNow.Fsu.Name,
                            Email = classNow.Fsu.Email
                        },
                        TrainingProgram = new ViewInfoClassTrainingProgramDto
                        {
                            TrainingProgramCode = classNow.TrainingProgramCodeNavigation?.TrainingProgramCode,
                            UpdatedBy = classNow.TrainingProgramCodeNavigation?.UpdatedBy,
                            UpdatedDate = classNow.TrainingProgramCodeNavigation?.UpdatedDate,
                            Days = classNow.TrainingProgramCodeNavigation?.Days,
                            Hours = classNow.TrainingProgramCodeNavigation?.Hours,
                            Name = classNow.TrainingProgramCodeNavigation?.Name
                        },
                        Syllabus = Syllabus
                    };
                    var response = new ResponseDto("Operation successful", 200, true, result);
                    return Ok(response);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting classes for Filter.");
                var response = new ResponseDto(
                    "Error retrieving data from the database",
                    500,
                    false,
                    ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("Week")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> GetListClassesByDateRange(
            DateOnly startDate,
            DateOnly endDate
        )
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
                var response = new ResponseDto(
                    "Error retrieving data from the database",
                    500,
                    false,
                    ex.Message
                );
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
                var response = new ResponseDto(
                    "Error retrieving data from the database",
                    500,
                    false,
                    ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        //Get the list syllabus remaining array from the training have syllabus before
        [HttpGet("ListSyllabusRemaining")]
        public async Task<IActionResult> GetListSyllabus([FromQuery] string trainingCode = null)
        {
            try
            {
                var listSyllabus = await _syllabusRepository.GetListSyllabus(trainingCode);
                return Ok(new ResponseDto("Successfull", 200, true, listSyllabus));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting class by time.");
                var response = new ResponseDto("Error retrieving data from the database", 500, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost("AddTrainingProgramSyllabus")]
        public async Task<IActionResult> AddProgramSyllabus([FromBody] InsertProgramSyllabusDTO modal)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Create fail", modal);
                var response = new ResponseDto("Modal is invalid", 400, false);
                return BadRequest(response);
            }
            try
            {
                _logger.LogInformation("Start Create Program Syllabus");
                var request = _mapper.Map<TrainingProgramSyllabus>(modal);
                var result = await _syllabusRepository.AddProgramSyllabus(request);
                if (result)
                {
                    _logger.LogInformation("Create Success");
                    var response = new ResponseDto("Add successfully", 201, true, request);
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("Fail to create");
                    var response = new ResponseDto("Something wrong while creating program syllabus!!!", 400, false);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                var response = new ResponseDto("Error", 500, false, e.Message);
                return BadRequest(response);
            }
        }
        [HttpPost("DuplicatedClass/{id}")]
        public async Task<IActionResult> DuplicatedClass(string id)
        {
            if (id == null)
            {
                _logger.LogError("Duplicate class fail");
                var response = new ResponseDto("Id is null", 400, false);
                return BadRequest(response);
            }
            try
            {
                _logger.LogInformation("Start Duplicate Class");
                var result = await _classRepository.DuplicatedClass(id);
                if (result)
                {
                    _logger.LogInformation("Duplicate Success");
                    var response = new ResponseDto("Duplicate successfully", 201, true, result);
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("Fail to Duplicate");
                    var response = new ResponseDto("Something wrong while Duplicate Class!!!", 400, false);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                var response = new ResponseDto("Error", 500, false, e.Message);
                return BadRequest(response);
            }

        }


        [HttpDelete("{trainingProgramCode}/{syllabusId}")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> DeleteTrainingProgramSyllabus(string trainingProgramCode, string syllabusId)
        {
            try
            {
                await _syllabusRepository.DeleteSyllabusCard(trainingProgramCode, syllabusId);
                var response = new ResponseDto("TrainingProgramSyllabus deleted successfully", 200, true);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete TrainingProgramSyllabus with TrainingProgramCode {trainingProgramCode} and SyllabusId {syllabusId}");
                var response = new ResponseDto("Failed to delete TrainingProgramSyllabus", 400, false);
                return BadRequest(response);
            }
        }

        [HttpPost("AddSyllabus")]
        public async Task<IActionResult> CreateSyllabus([FromBody] InsertSyllabus modal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto("Modal fail", 400, false));
            }
            try
            {
                var response = _mapper.Map<Syllabus>(modal);
                var result = await _syllabusRepository.CreateSyllabus(response);
                if (result)
                {
                    return Ok(new ResponseDto("Create Succesfull", 201, true, response));
                }
                else
                {
                    return BadRequest(new ResponseDto("Create fail", 400, false));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseDto("Error", 500, false, e));
            }
        }
        [HttpPut("{classId}/status")]
        public async Task<IActionResult> UpdateClassStatus(string classId, [FromBody] UpdateClassStatusDto classStatusData)
        {
            try
            {
                await _classRepository.UpdateClassStatus(classId, classStatusData.classStatus);
                var response = new ResponseDto($"Class status updated successfully", 200, true, null);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                var errorResponse = new ResponseDto(ex.Message, 404, false, null);
                return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseDto("Internal server error", 500, false, null);
                return StatusCode(500, errorResponse);
            }
        }
        [HttpGet("trainingProgram/{trainingProgramCode}/syllabuses")]
        public async Task<IActionResult> GetSyllabiByTrainingProgramCode(string trainingProgramCode)
        {
            try
            {
                var syllabi = await _syllabusRepository.GetSyllabiByTrainingProgramCode(trainingProgramCode);
                return Ok(syllabi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{classId}")]
        public async Task<IActionResult> UpdateClass(string classId, UpdatedClassDto updatedClassDto)
        {
            try
            {
                var existingClass = await _classRepository.GetClassById1(classId);

                if (existingClass == null)
                {
                    return NotFound("Class not found");
                }

                // Update class information
                existingClass.StartDate = updatedClassDto.StartDate;
                existingClass.EndDate = updatedClassDto.EndDate;
                existingClass.StartTime = updatedClassDto.StartTime;
                existingClass.EndTime = updatedClassDto.EndTime;
                existingClass.AcceptedAttendee = updatedClassDto.AcceptedAttendee;
                existingClass.ActualAttendee = updatedClassDto.ActualAttendee;
                existingClass.PlannedAttendee = updatedClassDto.PlannedAttendee;
                existingClass.AttendeeLevelId = updatedClassDto.AttendeeTypeName;
                existingClass.ClassId = updatedClassDto.ClassId;
                existingClass.Fsu = new Fsu
                {
                    FsuId = updatedClassDto.Fsu?.FsuId,
                    Name = updatedClassDto.Fsu?.Name,
                    Email = updatedClassDto.Fsu?.Email
                };
                existingClass.TrainingProgramCodeNavigation = new TrainingProgram
                {
                    TrainingProgramCode = updatedClassDto.TrainingProgram?.TrainingProgramCode,
                    UpdatedBy = updatedClassDto.TrainingProgram?.UpdatedBy,
                    UpdatedDate = updatedClassDto.TrainingProgram?.UpdatedDate,
                    Days = updatedClassDto.TrainingProgram?.Days,
                    Hours = updatedClassDto.TrainingProgram?.Hours,
                    Name = updatedClassDto.TrainingProgram?.Name
                };


                // Save changes
                await _classRepository.UpdateClass1(existingClass);

                // Return updated class details
                var updatedClass = await _classRepository.GetClassById1(classId);
                return Ok(updatedClass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserToClass([FromBody] AddUserToClassRequestDTO request)
        {
            try
            {
                await _classRepository.AddUserToClassAsync(request.UserId, request.ClassId);
                return Ok("User added to class successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserFromClass([FromBody] DeleteUserFromClassRequestDTO request)
        {
            try
            {
                await _userRepository.DeleteUserFromClassAsync(request.UserId, request.ClassId);
                return Ok("User deleted from class successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("AttendeeType")]
        public async Task<ActionResult<IEnumerable<AttendeeType>>> GetAttendeeTypes()
        {
            try
            {
                var attendeeTypes = await _classRepository.GetALlAttendeeTypeAsync();
                return Ok(attendeeTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
