using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScoreManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IStudentRepository _StudentRepository;
        private readonly IScoreRepository _ScoreRepository;

        public ScoreController(IStudentRepository studentRepository, IScoreRepository scoreRepository)
        {
            _StudentRepository = studentRepository;
            _ScoreRepository = scoreRepository;
        }


        public class ListScoreDTO
        {
            public List<ScoreDTO> data { get; set; }
            public int count { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        // GET: api/Score
        /*[HttpGet]
        public ActionResult<ListScoreDTO> Get()
        {
            List<ScoreDTO> listScoreDTO = new List<ScoreDTO>(); // list dto
            IEnumerable<Student> students = _StudentRepository.GetStudents(); // list sv
            bool scoresFound = false;

            foreach (Student student in students)
            {
                var score = _ScoreRepository.GetScoreDTOs(student);
                if (score != null)
                {
                    listScoreDTO.Add(score);
                    scoresFound = true;
                }
            }

            ListScoreDTO result = new ListScoreDTO
            {
                count = listScoreDTO.Count,
                data = listScoreDTO,
                Message = scoresFound ? null : "No scores found for the student."
            };

            return Ok(result);
        }*/

        /*[HttpPut("{StudentId}")]
        public IActionResult Put(string StudentId, ScoreUpdateDTO scoreUpdateDto)
        {
            

            if (StudentId != null && StudentId == scoreUpdateDto.StudentId)
            {
                scoreUpdateDto.StudentId = StudentId;
                return Ok(_ScoreRepository.UpdateScore(scoreUpdateDto));
            }

            return BadRequest("Fail");
        }*/

        [HttpGet("{classId}")]
        public ActionResult<ListScoreDTO> GetScoresByClassId(string classId)
        {
            var scores = _ScoreRepository.GetScoresByClassId(classId);
            if (!scores.Any())
            {
                return NotFound("No scores found for the specified class.");
            }

            var result = new ListScoreDTO
            {
                count = scores.Count(),
                data = scores.ToList(),
                IsSuccess = true, // Correctly set IsSuccess to true when scores exist
                Message = "Scores retrieved successfully."
            };

            return Ok(result);
        }
    }
}
