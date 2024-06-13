namespace TrainingProgramManagementAPI.Payloads.Requests;

public class ExportTrainingProgramRequest
{
    public string[]? Status { get; set; } = null!;
    public string? CreatedBy { get; set; } = string.Empty;
    public DateTime? ProgramTimeFrameFrom { get; set; } = null!;
    public DateTime? ProgramTimeFrameTo { get; set; } = null!;
    public int? _Page { get; set; } = 1;
    public string? _PerPage { get; set; } = string.Empty;
    public string? Sort { get; set; } = string.Empty;
}