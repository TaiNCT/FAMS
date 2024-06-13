using System.ComponentModel.DataAnnotations;

namespace TrainingProgramManagementAPI.Payloads.Requests;

public class DeleteMaterialRequest
{
    [Required]
    public Guid SyllabusId { get; set; } = Guid.Empty;

    [Required]
    public int DayNo { get; set; }

    [Required]
    public int UnitNo { get; set; }

    [Required]
    public int ChapterNo { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;
}