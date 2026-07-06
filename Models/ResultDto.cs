
namespace ObourLand.Models
{
    public class Result<T>
    {
        public int StatusCode { get; }
        public bool IsSuccess { get; }
        public string? Message { get; }
        public T? Value { get; }

        public Result(bool isSuccess, T value, string? message, int statusCode)
        {
            IsSuccess = isSuccess;
            Value = value;
            Message = message;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value, string? message = null)
            => new Result<T>(true, value, message, (int)Enums.StatusCode.Success);

        public static Result<T> Failure(string message, Enums.StatusCode statusCode = Enums.StatusCode.BadRequest)
            => new Result<T>(false, default, message, (int)statusCode);
    }
}
