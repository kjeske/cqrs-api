namespace CqrsApi.Infrastructure;

public record Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    internal Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    internal Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Ok() => new(true);
    public static Result<T> Ok<T>(T value) => new(true, value);
    public static Result<T> Fail<T>(string message, object? metadata = null) => new(new Error(message, metadata));
    public static Result Fail(Error error) => new(error);
}

public record Result<T> : Result
{
    public T Value
    {
        get;
    }

    public Result(bool isSuccess, T value)
        : base(isSuccess)
    {
        Value = value;
    }

    public Result(Error error)
        : base(error)
    {
    }
}

public record Error(string Message, object? Metadata = null);