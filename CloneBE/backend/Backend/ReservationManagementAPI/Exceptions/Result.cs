using FluentResults;
namespace ReservationManagementAPI.Exceptions
{
    public class Result
    {
        private Result(bool isSuccess, Message error)
        {
            if (!isSuccess && error == Message.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Message Error { get; }

        public static Result Success() => new(true, Message.None);

        public static Result Failure(Message error) => new(false, error);

        public static Result Success(Message message) => new(true, message);
    }
}
