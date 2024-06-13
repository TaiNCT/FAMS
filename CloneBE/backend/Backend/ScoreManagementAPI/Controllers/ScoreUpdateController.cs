using AutoMapper;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.Interfaces;
using ScoreManagementAPI.Repository;
using ScoreManagementAPI.Singleton;

namespace ScoreManagementAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ScoreUpdateController : ControllerBase
   {
        private readonly IStudentRepository _StudentRepository;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly FamsContext _context;
        private readonly Store _store;
        public ScoreUpdateController(IStudentRepository StudentRepository, ILogger<StudentController> logger, IMapper mapper, FamsContext context, Store store)
        {
            _StudentRepository = StudentRepository;
            _logger = logger;
            _mapper = mapper;
            _store = store;
            _context = context;
        }

        [HttpPost("update/{id}", Name = "UpdateStudentsScore")]
        public ActionResult UpdateStudentScore(string id, IStudentUpdate data)
        {
            // Check if the student ID is null or empty
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Student ID cannot be empty.");
            }

            // Check if the studentUpdateData is null
            if (data == null)
            {
                return BadRequest("Student update data cannot be null.");
            }

            // Extract the student based on the UUID
            Student std = _context.Students.FirstOrDefault(s => s.StudentId == id);

            if (std == null)
            {
                return NotFound();
            }

            try
            {
                // Update the information on the student
                _StudentRepository.UpdateStudentBasedOnJSON(std, data);
                return Ok(new { msg = "Successfully updated." });
            }
            catch (Exception ex)
            {
                // Log the exception and return an Internal Server Error
                _logger.LogError($"Error updating student score: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error");
            }
        }

    }
}
