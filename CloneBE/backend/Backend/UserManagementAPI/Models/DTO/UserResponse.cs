namespace UserManagementAPI.Models.DTO
{
    public class UserResponse
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
        public int StatusCode { get; set; }
        public object? Data { get; set; } = null!;
        public IDictionary<string, string[]> Errors { get; set; } = null!;

    }
}
