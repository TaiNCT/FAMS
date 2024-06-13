namespace EmailInformAPI.DTO;

public class AddEmailTemplateDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Type { get; set; }
    public string ApplyTo { get; set; } = null!;

}