using Entities.Models;

namespace ReservationManagementAPI.Entities.DTOs
{
    public class StudentReservedDTO
    {
        public string ReservedClassId { get; set; }

        public string StudentId { get; set; }

        public string MutatableStudentId { get; set; }

        public string ClassId { get; set; }

        public string Reason { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string ClassName { get; set; }
        public string ModuleName { get; set; }
        public string StudentName { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        public string Email {  get; set; }

        public string ClassEndDate { get; set; }

        public DateTime CreatedDate {  get; set; }
    }
}
