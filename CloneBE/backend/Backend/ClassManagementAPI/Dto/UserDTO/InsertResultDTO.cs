namespace ClassManagementAPI.Dto.UserDTO
{
    public class InsertResultDTO
    {
        public string UserId { get; set; } = null!;

        public string ClassId { get; set; } = null!;

        public string? UserType { get; set; }
    }
}
