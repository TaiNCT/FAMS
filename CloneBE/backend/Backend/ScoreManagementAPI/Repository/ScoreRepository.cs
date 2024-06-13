using Microsoft.AspNetCore.Mvc;
using Entities.Context;
using ScoreManagementAPI.DAO;
using ScoreManagementAPI.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly FamsContext _context;
        public ScoreRepository(FamsContext context)
        {
            _context = context;
        }
        public IEnumerable<Score> GetAssignmentStudents(string StudentId)
        => AssignmentDAO.GetAssignmentStudents(StudentId);

        public IEnumerable<StudentModule> GetModuleStudents(string StudentId)
        => ModuleDAO.GetModuleStudents(StudentId);

        public List<QuizStudent> GetQuizStudents(string StudentId)
        => QuizDAO.GetQuizStudents(StudentId);

        public ScoreDTO GetScoreDTOs(Student student)
        {
            ScoreDTO ret = new ScoreDTO();

            // Common information
            ret.FullName = student.FullName;
            ret.Faaccount = student.Faaccount;
            ret.audit = student.Audit;
            ret.gpa1 = (double)student.Gpa;
            ret.gpa2 = (double)student.Gpa;
            ret.mock = student.Mock;
            ret.uuid = student.StudentId;

            // Building a common query to QuizStudents table
            var qs = _context.QuizStudents.Where(qs => qs.StudentId.Equals(student.StudentId)).OrderBy(e => e.SubmissionDate);

            // Getting quiz scores
            ret.html = (double?)qs.Where(qs => qs.Quiz.QuizName.Equals("HTML")).Select(qs => qs.Score).FirstOrDefault();
            ret.css = (double?)qs.Where(qs => qs.Quiz.QuizName.Equals("CSS")).Select(qs => qs.Score).FirstOrDefault();
            ret.quiz3 = (double?)qs.Where(qs => qs.Quiz.QuizName.Equals("Quiz 3")).Select(qs => qs.Score).FirstOrDefault();
            ret.quiz4 = (double?)qs.Where(qs => qs.Quiz.QuizName.Equals("Quiz 4")).Select(qs => qs.Score).FirstOrDefault();
            ret.quiz5 = (double?)qs.Where(qs => qs.Quiz.QuizName.Equals("Quiz 5")).Select(qs => qs.Score).FirstOrDefault();
            ret.quiz6 = (double?)qs.Where(qs => qs.Quiz.QuizName.Equals("Quiz 6")).Select(qs => qs.Score).FirstOrDefault();
            ret.quizfinal = (double?)qs.Where(qs => qs.Quiz.QuizName.Equals("Quiz Final")).Select(qs => qs.Score).FirstOrDefault();

            // Getting assignments scores ( Practice 1, Practice 2, etc )
            var score = _context.Scores.Where(s => s.StudentId.Equals(student.StudentId)).OrderBy(e => e.SubmissionDate);
            ret.practice1 = (double?)score.Where(s => s.Assignment.AssignmentName.Equals("Practice 1")).Select(s => s.Score1).FirstOrDefault();
            ret.practice2 = (double?)score.Where(s => s.Assignment.AssignmentName.Equals("Practice 2")).Select(s => s.Score1).FirstOrDefault();
            ret.practice3 = (double?)score.Where(s => s.Assignment.AssignmentName.Equals("Practice 3")).Select(s => s.Score1).FirstOrDefault();
            ret.pracfinal = (double?)score.Where(s => s.Assignment.AssignmentName.Equals("Practice Final")).Select(s => s.Score1).FirstOrDefault();

            // Getting level scores 
            var studentmod = _context.StudentModules.Where(s => s.StudentId.Equals(student.StudentId)).OrderBy(sm => sm.StudentModuleId);
            ret.level1 = studentmod.Select(s => s.ModuleLevel).FirstOrDefault();
            ret.level2 = studentmod.Select(s => s.ModuleLevel).Skip(1).FirstOrDefault();

            return ret;
        }

        /*=> ScoreDAO.GetScoreDTOs(student); *//*, quizStudents, scores, studentModules*/

        public IEnumerable<ScoreDTO> GetScoresByClassId(string classId)
        {
            // Query the StudentClasses table to find all students in the specified class
            var studentClasses = _context.StudentClasses
        .Where(sc => sc.ClassId == classId)
        .Include(sc => sc.Student)
            .ThenInclude(s => s.QuizStudents.OrderByDescending(qs => qs.SubmissionDate))
            .ThenInclude(qs => qs.Quiz)
        .Include(sc => sc.Student)
            .ThenInclude(s => s.Scores.OrderByDescending(s => s.SubmissionDate))
            .ThenInclude(s => s.Assignment)
        .Include(sc => sc.Student)
            .ThenInclude(s => s.StudentModules)
        .ToList();

            // Project the query results into a collection of ScoreDTO objects
            var scores = studentClasses.Select(sc => new ScoreDTO
            {
                FullName = sc.Student.FullName,
                Faaccount = sc.Student.Faaccount,
                audit = sc.Student.Audit,
                gpa1 = (double)sc.Student.Gpa,
                gpa2 = (double)sc.Student.Gpa,
                mock = sc.Student.Mock,
                uuid = sc.Student.StudentId,
                // Quiz scores
                html = (double?)sc.Student.QuizStudents.Where(qs => qs.Quiz.QuizName.Equals("HTML")).Select(qs => qs.Score).FirstOrDefault(),
                css = (double?)sc.Student.QuizStudents.Where(qs => qs.Quiz.QuizName.Equals("CSS")).Select(qs => qs.Score).FirstOrDefault(),
                quiz3 = (double?)sc.Student.QuizStudents.Where(qs => qs.Quiz.QuizName.Equals("Quiz 3")).Select(qs => qs.Score).FirstOrDefault(),
                quiz4 = (double?)sc.Student.QuizStudents.Where(qs => qs.Quiz.QuizName.Equals("Quiz 4")).Select(qs => qs.Score).FirstOrDefault(),
                quiz5 = (double?)sc.Student.QuizStudents.Where(qs => qs.Quiz.QuizName.Equals("Quiz 5")).Select(qs => qs.Score).FirstOrDefault(),
                quiz6 = (double?)sc.Student.QuizStudents.Where(qs => qs.Quiz.QuizName.Equals("Quiz 6")).Select(qs => qs.Score).FirstOrDefault(),
                quizfinal = (double?)sc.Student.QuizStudents.Where(qs => qs.Quiz.QuizName.Equals("Quiz Final")).Select(qs => qs.Score).FirstOrDefault(),
                // Assignment scores
                practice1 = (double?)sc.Student.Scores.Where(s => s.Assignment.AssignmentName.Equals("Practice 1")).Select(s => s.Score1).FirstOrDefault(),
                practice2 = (double?)sc.Student.Scores.Where(s => s.Assignment.AssignmentName.Equals("Practice 2")).Select(s => s.Score1).FirstOrDefault(),
                practice3 = (double?)sc.Student.Scores.Where(s => s.Assignment.AssignmentName.Equals("Practice 3")).Select(s => s.Score1).FirstOrDefault(),
                pracfinal = (double?)sc.Student.Scores.Where(s => s.Assignment.AssignmentName.Equals("Practice Final")).Select(s => s.Score1).FirstOrDefault(),
                // Level scores
                level1 = sc.Student.StudentModules.Select(s => s.ModuleLevel).FirstOrDefault(),
                level2 = sc.Student.StudentModules.Skip(1).Select(s => s.ModuleLevel).FirstOrDefault()
            });

            return scores;
        }
        public IActionResult UpdateScore(ScoreUpdateDTO scoreUpdateDTO)
        => ScoreDAO.UpdateScoreDTO(scoreUpdateDTO);
    }
}
