using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class TrainingMaterialForCreationDTO
    {
        public string? CreatedBy { get; set; }

        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; } = null!;

        [Required(ErrorMessage = "IsFile is required")]
        public bool IsFile { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;

        public string? Url { get; set; }
    }
}