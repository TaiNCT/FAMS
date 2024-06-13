using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class SyllabusDayForCreationDTO
    {
        public string? CreatedBy { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int? DayNo { get; set; }

        public virtual ICollection<SyllabusUnitForCreationDTO> SyllabusUnits { get; set; } = new List<SyllabusUnitForCreationDTO>();
    }
}