namespace StudentInfoManagementAPI.DTO
{
    public class ClassDTO
    {
        public string ClassId { get; set; } = null!;

        public int Id { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string ClassStatus { get; set; } = null!;

        public string ClassCode { get; set; } = null!;

        public int? Duration { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public string? ReviewBy { get; set; }

        public DateTime? ReviewDate { get; set; }

        public int AcceptedAttendee { get; set; }

        public int ActualAttendee { get; set; }

        public string ClassName { get; set; } = null!;

        public string? FsuId { get; set; }

        public string? LocationId { get; set; }

        public string? AttendeeLevelId { get; set; }

        public string? TrainingProgramCode { get; set; }

        public int PlannedAttendee { get; set; }

        public string? SlotTime { get; set; }
    }
}
