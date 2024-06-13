using Entities.Models;

namespace ClassManagementAPI.Dto.ClassDTO
{
    public class UpdatedClassDto
    {
        public double? TotalHours { get; set; }
        public int? TotalDays { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string ClassCode { get; set; }
        public string ClassStatus { get; set; }
        public string ClassName { get; set; }
        public string ClassId { get; set; }
        public string? ReviewBy { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AcceptedAttendee { get; set; }
        public int? ActualAttendee { get; set; }
        public int? PlannedAttendee { get; set; }
        public string? AttendeeTypeName { get; set; }
        public List<ViewInfoClassUserDto>? Users { get; set; }
        public ViewInfoClassFsuDto? Fsu { get; set; }
        public ViewInfoClassTrainingProgramDto? TrainingProgram { get; set; }

    }

    public class updatedClassUserDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class updatedTrainingProgramDto
    {
        public string TrainingProgramCode { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Name { get; set; }
    }
    public class updatedClassFsuDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }


}



