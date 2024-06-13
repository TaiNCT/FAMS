using Entities.Models;

namespace TrainingProgramManagementAPI.DTOs;

public class TrainingMaterialDto()
{
  public int Id { get; set; }

  public string TrainingMaterialId { get; set; } = null!;

  public string? CreatedBy { get; set; }

  public DateTime? CreatedDate { get; set; }

  public bool IsDeleted { get; set; }

  public string? ModifiedBy { get; set; }

  public DateTime? ModifiedDate { get; set; }

  public string FileName { get; set; } = null!;

  public bool IsFile { get; set; }

  public string Name { get; set; } = null!;

  public string? Url { get; set; }

  public string? UnitChapterId { get; set; }

}