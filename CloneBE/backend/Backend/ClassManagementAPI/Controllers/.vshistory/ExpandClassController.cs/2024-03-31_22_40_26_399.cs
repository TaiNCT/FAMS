﻿using AutoMapper;
using ClassManagementAPI.Dto.UserDTO;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using ClassManagementAPI.Repositories;
using Entities.Context;

namespace ClassManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpandClassController : ControllerBase
    {
        private readonly FamsContext _context;
        private readonly IElasticClient _client;
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassUserRepository _classUserRepository;
        private readonly ISyllabusRepository _syllabusRepository;
        private readonly ITrainerRepository _trainingProgramRepository;
        private readonly IClassUserRepository _classUser;

        public ExpandClassController(ILogger<ClassController> logger, IElasticClient client, IMapper mapper, IClassRepository classRepository, ILocationRepository locationRepository, IUserRepository userRepository, IClassUserRepository classUserRepository, ISyllabusRepository syllabusRepository, ITrainerRepository trainingProgramRepository)

        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
            _userRepository = userRepository;
            _classUserRepository = classUserRepository;
            _syllabusRepository = syllabusRepository;
            _trainingProgramRepository = trainingProgramRepository;
        }


        [HttpGet("GetTrainingProgramList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TrainingProgram>))]
        public async Task<IActionResult> GetTrainingProgramList()
        {
            _logger.LogInformation("Success");
            var trainingList = await _trainingProgramRepository.GetAllTraningProgramList();
            var respone = new
            {
                trainingList
            };
            return Ok(respone);
        }

        [HttpGet("GetUserBasic")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> GetUserBasic()
        {
            _logger.LogInformation("Success");
            var userList = await _userRepository.GetAll();
            var roleList = await _userRepository.GetAllRole();
            var userBasicDto = (from user in userList
                                join role in roleList
                                on user.RoleId equals role.RoleId
                                where role.RoleName == "Admin"
                                select new
                                {
                                    userId = user.UserId,
                                    fullName = user.FullName,
                                }).ToList();

            var response = new ResponseDto("Get List of Basic User Successfully", 200, true, new { userBasicDto });
            return Ok(response);
        }

        [HttpPost("CreateclassUser")]
        [ProducesResponseType(200, Type = typeof(ResponseDto))]
        public async Task<IActionResult> AddClassUser([FromQuery] InsertClassUserDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("CLASSUSER CAN NOT BE NULL!");
                }
                var classUser = await _classUserRepository.AddClassUser(dto);

                var mapper = _mapper.Map<InsertResultDTO>(classUser);
                var response = new ResponseDto("Get List of Basic User Successfully", 200, true, mapper);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Fail");
                var response = new ResponseDto(ex.Message, 400, false);
                return BadRequest(response);
            }
        }

        [HttpGet("GetSyllabiByTrainingProgramCode/{trainingProgramCode}")]
        public async Task<IActionResult> GetSyllabiByTrainingProgramCode(string trainingProgramCode)
        {
            try
            {
                var syllabi = await _syllabusRepository.GetSyllabiByTrainingProgramCode(trainingProgramCode);
                //var result = _mapper.Map<PagedResult<GetSyllabusDTO>>(syllabi);
                return Ok(syllabi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetSyllabiByTrainingProgramCode)}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
