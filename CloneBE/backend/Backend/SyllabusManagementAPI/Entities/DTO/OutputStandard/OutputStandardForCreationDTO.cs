using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class OutputStandardForCreationDTO
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Descriptions is required")]
        public string Descriptions { get; set; } = null!;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
    }
}