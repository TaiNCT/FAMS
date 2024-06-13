using AutoMapper;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Context;
using ScoreManagementAPI.DAO;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Interfaces;
using Entities.Models;
using ScoreManagementAPI.Repository;

namespace ScoreManagementAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _StudentRepository;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly FamsContext _context;

        public StudentController(IStudentRepository StudentRepository, ILogger<StudentController> logger, IMapper mapper, FamsContext context)
        {
            _StudentRepository = StudentRepository;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("all", Name = "GetStudents")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            try
            {
                var students = _StudentRepository.GetStudents();
                if (students == null || !students.Any())
                {
                    return NotFound(); // or return an empty list if desired
                }

                var studentDTOs = _mapper.Map<List<StudentDTO>>(students);
                if (studentDTOs == null)
                {
                    _logger.LogError("Mapper returned null for students");
                    return StatusCode(500); // or return an appropriate error response
                }

                return Ok(studentDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching students");
                return StatusCode(500); // or return an appropriate error response
            }
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        public IActionResult GetStudentById(string id)
        {
            try
            {
                // Query Student by UUID id, not the integer one
                StudentDTO Student = _StudentRepository.GetStudentByID(id);

                // Check if the Student object is null before accessing its properties
                if (Student == null)
                {
                    return NotFound($"Student with ID {id} not found!");
                }

                Student.quizes = _StudentRepository.getQuizByStudentID(id);
                Student.asm = _StudentRepository.getPracticesByStudentID(id);

                return Ok(Student);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while fetching student by ID");
                // Return an InternalServerError status code
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
