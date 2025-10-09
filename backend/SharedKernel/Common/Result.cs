namespace SharedKernel.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T? Value { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, T? value, string? error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("A successful result cannot have an error message.");

            if (!isSuccess && value != null && !IsNullableType(typeof(T)))
                throw new InvalidOperationException("A failed result cannot have a value.");

            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);

        public static Result<T> Failure(string error) => new Result<T>(false, default, error);

        private static bool IsNullableType(Type type) =>
            !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result Failure(string error) => new Result(false, error);
    }
}
