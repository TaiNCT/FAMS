namespace UserManagementAPI.Models.DTO
{
    public class ImportNewRoleDTO
    {
        public string RoleName { get; set; }
        public string Title { get; set; } = null!;
        public string Syllabus { get; set; }
        public string TrainingProgram { get; set; }
        public string Class { get; set; }
        public string LearningMaterial { get; set; }
        public string UserManagement { get; set; }
    }
}
