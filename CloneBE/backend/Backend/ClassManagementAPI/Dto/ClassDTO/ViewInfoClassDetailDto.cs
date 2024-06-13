using Entities.Models;

namespace ClassManagementAPI.Dto.ClassDTO
{
    public class ViewInfoClassDetailDto
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
        public string? LocationName { get; set; }
        public List<ViewInfoClassUserDto>? Users { get; set; }
        public ViewInfoClassFsuDto? Fsu { get; set; }
        public ViewInfoClassTrainingProgramDto? TrainingProgram { get; set; }
        public List<ViewInfoClassSyllabusDto>? Syllabus { get; set; }
    }

    public class ViewInfoClassUserDto
    {
        public string UserId { get; set; }
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class ViewInfoClassTrainingProgramDto
    {
        public string TrainingProgramCode { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Name { get; set; }
    }
    public class ViewInfoClassFsuDto
    {
        public string FsuId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class ViewInfoClassSyllabusDto
    {
        public string? SyllabusId { get; set; }
        public string? TopicName { get; set; }
        public string? Version { get; set; }
        public string? TopicCode { get; set; }
        public int? Days { get; set; }
        public double? Hours { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string? Status { get; set; }
    }
}
