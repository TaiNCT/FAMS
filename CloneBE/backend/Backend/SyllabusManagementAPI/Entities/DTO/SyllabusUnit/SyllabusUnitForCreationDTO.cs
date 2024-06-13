using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class SyllabusUnitForCreationDTO
    {
        public string? CreatedBy { get; set; }

        public int? Duration { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Unit number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int UnitNo { get; set; }

        public virtual ICollection<UnitChapterForCreationDTO> UnitChapters { get; set; } = new List<UnitChapterForCreationDTO>();
    }
}