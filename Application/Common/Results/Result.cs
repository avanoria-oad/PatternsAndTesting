namespace Application.Common.Results;

public sealed record Result(bool Success, ErrorTypes? ErrorTypes = null, string? ErrorMessage = null)
{
    public static Result Ok() => new(true);

    public static Result BadRequest(string message) => new(false, Results.ErrorTypes.BadRequest, message);
    public static Result NotFound(string message) => new(false, Results.ErrorTypes.NotFound, message);
    public static Result Conflict(string message) => new(false, Results.ErrorTypes.Conflict, message);
    public static Result Error(string message = "An unexpected error occurred.") 
        => new(false, Results.ErrorTypes.Unexpected, message);
}

public sealed record Result<T>(bool Success, T? Value = default, ErrorTypes? ErrorTypes = null, string? ErrorMessage = null)
{
    public static Result<T> Ok(T value) => new(true, value);

    public static Result<T> BadRequest(string message) => new(false, default, Results.ErrorTypes.BadRequest, message);
    public static Result<T> NotFound(string message) => new(false, default, Results.ErrorTypes.NotFound, message);
    public static Result<T> Conflict(string message) => new(false, default, Results.ErrorTypes.Conflict, message);
    public static Result<T> Error(string message = "An unexpected error occurred.")
        => new(false, default, Results.ErrorTypes.Unexpected, message);
}
