using Microsoft.AspNetCore.Http;

namespace UserManagementAPI.Models.DTO
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public int StatusCode { get; set; }

        public ResponseDTO()
        {
            IsSuccess = true;
        }

        public ResponseDTO(string message, int statusCode, bool success = false, object? result = null)
        {
            IsSuccess = success;
            Message = message;
            Result = result;
            StatusCode = statusCode;
        }
    }
}