namespace TrainingProgramManagementAPI.DTOs
{
    public class TrainingProgramDto
    {
        // UUID 
        public string TrainingProgramCode { get; set; } = null!;

        // Identity ID

        public int Id { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? Days { get; set; } = 0;

        public int? Hours { get; set; } = 0;

        public TimeOnly StartTime { get; set; }

        public string Name { get; set; } = null!;

        public string Status { get; set; } = TrainingProgramStatus.Draft.ToString();

        public string? UserId { get; set; }

        public string? TechnicalCodeId { get; set; }

        public string? TechnicalGroupId { get; set; }

        public virtual ICollection<ClassDto> Classes { get; set; } = new List<ClassDto>();

        public virtual TechnicalCodeDto? TechnicalCode { get; set; }

        public virtual TechnicalGroupDto? TechnicalGroup { get; set; }

        public virtual UserDto? User { get; set; }

        public virtual ICollection<SyllabusDto> Syllabi { get; set; } = new List<SyllabusDto>();
    }
}