using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SyllabusManagementAPI.Entities.DTO.UnitChapter.Validation;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class UnitChapterForCreationDTO
    {
        public string? CreatedBy { get; set; }

        [Required(ErrorMessage = "ChapterNo is required")]
        public int ChapterNo { get; set; }

        public int? Duration { get; set; }

        [Required(ErrorMessage = "IsOnline is required")]
        public bool IsOnline { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Delivery Type is required")]
        //[DeliveryType(ErrorMessage = "Invalid Delivery Type")] //need to fix
        public string? DeliveryTypeId { get; set; }

        [Required(ErrorMessage = "Output Standard is required")]
        //[OutputStandard(ErrorMessage = "Invalid Output Standard")]
        public string? OutputStandardId { get; set; }

        public virtual ICollection<TrainingMaterialForCreationDTO> TrainingMaterials { get; set; } = new List<TrainingMaterialForCreationDTO>();
    }
}