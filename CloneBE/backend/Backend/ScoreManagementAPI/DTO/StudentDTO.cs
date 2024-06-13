
namespace ScoreManagementAPI.DTO
{
    public class IQuiz
    {
        public double? html { get; set; }
        public double? css { get; set; }
        public double? quiz3 { get; set; }
        public double? quiz4 { get; set; }
        public double? quiz5 { get; set; }
        public double? quiz6 { get; set; }
        public double? quizfinal { get; set; }

    }

    public class IAsm
    {
        public double? practice1 {  get; set; }
        public double? practice2 { get; set; }
        public double? practice3 { get;set; }
        public double? practice_final { get; set; }
    }


    public class StudentDTO
    {
        public string Id { get; set; }

        public string StudentId { get; set; }

        public string FullName { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public string Major { get; set; }

        public string GraduatedDate { get; set; }

        public double Gpa { get; set; }

        public string Address { get; set; } = null!;

        public string Faaccount { get; set; } = null!;

        public int Type { get; set; }

        public double Mock { get; set; }

        public int Audit { get; set; }

        public string Status { get; set; }

        public DateTime JoinedDate { get; set; }

        public string Area { get; set; } = null!;

        public string University { get; set; } = null!;

        public string RECer { get; set; } = null!;

        public string PermanentRes {  get; set; } = null!;

        public string Location { get; set; } = null!;


        public IQuiz quizes { get; set; }
        public IAsm asm { get; set; }
    }
}
