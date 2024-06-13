using Entities.Context;
using Entities.Models;

namespace ScoreManagementAPI.DAO
{
    public class AssignmentDAO
    {
        public static IEnumerable<Score> GetAssignmentStudents(string StudentId)
        {
            List<Score> assStudents = new List<Score>();
            try
            {
                using (var context = new FamsContext())
                {
                   /* assStudents = context.Score.Where(q => q.StudentId == StudentId).ToList();*/
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return assStudents;
        }
    }
}
