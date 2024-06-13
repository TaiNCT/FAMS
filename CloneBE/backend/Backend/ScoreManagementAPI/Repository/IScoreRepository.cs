using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.DTO;
using Entities.Models;

namespace ScoreManagementAPI.Repository
{
    public interface IScoreRepository
    {
        public List<QuizStudent> GetQuizStudents(string StudentId);
        public IEnumerable<Score> GetAssignmentStudents(string StudentId);
        public IEnumerable<StudentModule> GetModuleStudents(string StudentId);
        /*public ScoreDTO GetScoreDTOs(Student student, IEnumerable<QuizStudent> quizStudents
            , IEnumerable<Score> scores, IEnumerable<StudentModule> studentModules);
*/
        public IEnumerable<ScoreDTO> GetScoresByClassId(string classId);
        public ScoreDTO GetScoreDTOs(Student student);
        public IActionResult UpdateScore(ScoreUpdateDTO scoreUpdateDTO);

    }

}