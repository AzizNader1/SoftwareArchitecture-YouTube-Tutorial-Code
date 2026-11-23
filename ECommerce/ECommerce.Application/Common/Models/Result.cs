namespace ECommerce.Application.Common.Models
{
    public class Result
    {
        protected Result(bool isSuccess, string error, string[]? errors = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            Errors = errors ?? Array.Empty<string>();
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }
        public string[] Errors { get; }

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error, string[]? errors = null) => new(false, error, errors);
    }

    public sealed class Result<T> : Result
    {
        private Result(bool isSuccess, T? value, string error, string[]? errors = null)
            : base(isSuccess, error, errors)
        {
            Value = value;
        }

        public T? Value { get; }

        public static Result<T> Success(T value) => new(true, value, string.Empty);
        public static new Result<T> Failure(string error, string[]? errors = null) => new(false, default, error, errors);

        public static implicit operator Result<T>(T value) => Success(value);
    }
}
