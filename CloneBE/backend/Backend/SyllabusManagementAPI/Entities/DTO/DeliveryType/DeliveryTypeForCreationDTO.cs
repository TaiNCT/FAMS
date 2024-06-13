using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class DeliveryTypeForCreationDTO
    {
        [Required(ErrorMessage = "Descriptions is required")]
        public string Descriptions { get; set; } = null!;

        public string? Icon { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
    }
}