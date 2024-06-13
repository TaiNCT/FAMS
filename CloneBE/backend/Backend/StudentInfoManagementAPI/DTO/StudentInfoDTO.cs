using Entities.Models;

namespace StudentInfoManagementAPI.DTO
{
    public class StudentInfoDTO
    {
        public string StudentId { get; set; } = null!;

        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string Gender { get; set; } = null!;

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public string MajorId { get; set; } = null!;

        public DateTime GraduatedDate { get; set; }

        public decimal Gpa { get; set; }

        public string Address { get; set; } = null!;

        public string Faaccount { get; set; } = null!;

        public int Type { get; set; }

        public string Status { get; set; }

        public DateTime JoinedDate { get; set; }

        public string Area { get; set; } = null!;

        public string? Recer { get; set; }

        public string? University { get; set; }

        public static implicit operator StudentInfoDTO(Student v)
        {
            throw new NotImplementedException();
        }
    }
}
