namespace UserManagementAPI.Models.DTO
{
    public class RolePermissionsDetails
    {
        public string RoleId { get; set; }
        public string Syllabus { get; set; }
        public string TrainingProgram { get; set; }
        public string Class { get; set; }
        public string LearningMaterial { get; set; }
        public string UserManagement { get; set; }
    }

    public class RolePermissionUpdateDTO
    {
        public List<RolePermissionsDetails> RolePermissions { get; set; }
    }

}
