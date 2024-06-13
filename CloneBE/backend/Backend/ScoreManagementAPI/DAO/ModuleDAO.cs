using Entities.Models;

namespace ScoreManagementAPI.DAO
{
    public class ModuleDAO
    {
        public static IEnumerable<StudentModule> GetModuleStudents(string StudentId)
        {
            List<StudentModule> moduleStudents = new List<StudentModule>();
            try
            {
                /*using (var context = new FamsContext())
                {
                    moduleStudents = context.Student.Where(q => q.StudentId == StudentId).ToList();
                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return moduleStudents;
        }
    }
}
