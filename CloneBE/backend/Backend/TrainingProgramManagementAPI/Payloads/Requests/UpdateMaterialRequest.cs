using System.ComponentModel.DataAnnotations;

namespace TrainingProgramManagementAPI.Payloads.Requests;

public class UpdateMaterialRequest
{
    [Required]
    public string SyllabusId { get; set; } = string.Empty;

    [Required]
    public string TrainingMaterialId { get; set; } = string.Empty;

    [Required]
    public int DayNo { get; set; }

    [Required]
    public int UnitNo { get; set; }

    [Required]
    public int ChapterNo { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string ModifiedBy { get; set; } = string.Empty;

    public IFormFile? File { get; set; } = null!;
}