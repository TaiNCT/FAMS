namespace ClassManagementAPI.Dto.UserDTO
{
    public class InsertClassUserDTO
    {
        public string ClassId { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public string? UserType { get; set; }
    }
}
