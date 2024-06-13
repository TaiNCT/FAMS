using Entities.Context;
using Entities.Models;

namespace ScoreManagementAPI.DAO
{
    public class QuizDAO
    {
        public static List<QuizStudent> GetQuizStudents(string StudentId)
        {
            List<QuizStudent> quizStudents = new List<QuizStudent>();
            FamsContext context = new FamsContext();

            quizStudents = context.QuizStudents.Where(q => q.StudentId.Equals(StudentId)).ToList();

            return quizStudents;
        }
    }
}
