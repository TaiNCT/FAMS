using System.Text.Json;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class ResponseDTO
    {
        public int StatusCode { get; set; } = 200;
        public string? Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = true;
        public ResultDTO? Result { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
