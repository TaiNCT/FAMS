using Entities.Models;

namespace ClassManagementAPI.Dto.ClassDTO
{
    public class UpdatedClassDto
    {
        public string ClassId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }   
        public int AcceptedAttendee { get; set; }
        public int ActualAttendee { get; set; }
        public int PlannedAttendee { get; set; }
        public string? AttendeeTypeName { get; set; }
        public updatedClassUserDto? Users { get; set; }
        public updatedClassFsuDto? Fsu { get; set; }
        public updatedTrainingProgramDto? TrainingProgram { get; set; }

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



