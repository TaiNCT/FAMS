namespace StudentInfoManagementAPI.DTO
{
    public class UpdateStatusRequestDTO
    {
        public List<string> StudentIds { get; set; }
        public string NewStatus { get; set; }
    }
}