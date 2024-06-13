using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using System.Net;

namespace SyllabusManagementAPI.Middleware
{
    public class ResponseHandler
    {
        private ILoggerManager _logger;

        public ResponseHandler(ILoggerManager logger)
        {
            _logger = logger;
        }

        public ResponseDTO CreateReponse(int statusCode, string message, bool isSuccess, ResultDTO result)
        {
            return new ResponseDTO
            {
                StatusCode = statusCode,
                Message = message,
                IsSuccess = isSuccess,
                Result = result,
            };
        }

        // Success responses
        public ResponseDTO GetSuccessResponse(string message, ResultDTO result)
        {
            _logger.LogInfo(message);
            return CreateReponse((int)HttpStatusCode.OK, message, true, result);
        }

        public ResponseDTO CreateSuccessResponse(string message, ResultDTO result)
        {
            _logger.LogInfo(message);
            return CreateReponse((int)HttpStatusCode.Created, message, true, result);
        }

        public ResponseDTO UpdateSuccessResponse(string message, ResultDTO result)
        {
            _logger.LogInfo(message);
            return CreateReponse((int)HttpStatusCode.NoContent, message, true, result);
        }

        public ResponseDTO DeleteSuccessResponse(string message, ResultDTO result)
        {
            _logger.LogInfo(message);
            return CreateReponse((int)HttpStatusCode.NoContent, message, true, result);
        }
    }
}
