using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScoreManagementAPI.DTO
{
        public class ScoreDTO
        {
                public string FullName { get; set; } = null!;
                public string Faaccount { get; set; } = null!;
                public string uuid {  get; set; } = null!;        
                public double? html { get; set; }
                public double? css { get; set; }
                public double? quiz3 { get; set; }
                public double? quiz4 { get; set; }
                public double? quiz5 { get; set; }
                public double? quiz6 { get; set; }
                public double? quizfinal { get; set; }
                public double? practice1 { get; set; }
                public double? practice2 { get; set; }
                public double? practice3 { get; set; }

                public double? pracfinal { get; set; }

                 public double? audit { get; set; }
                public double? gpa1 { get; set; }
                public int? level1 { get; set; }
                public double? mock { get; set; }
                public double? gpa2 { get; set; }
                public int? level2 { get; set; }

                /*        public IEnumerable<QuizStudentDTO> QuizStudent { get; set; } = null!;
                        public IEnumerable<AssignmentStudentDTO> AssignmentStudent { get; set; } = null!;*/
                /*        public IEnumerable<ModuleStudentDTO> ModuleStudents { get; set; } = null!;*/
        }
}
