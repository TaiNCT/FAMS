namespace ClassManagementAPI.Dto.ClassDTO
{
    public class updatedTrainingProgramInClassDto
    {
        public string TrainingProgramCode { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public string Name { get; set; }
    }
}
