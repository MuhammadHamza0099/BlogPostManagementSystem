namespace BPMS.Result;

public class Result : IResult
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public bool Failed => !Succeeded;

    public Result() { Succeeded = false; Message = string.Empty; Errors = new List<string>(); }

    public static IResult Success()
    {
        return new Result { Succeeded = true };
    }

    public static IResult Success(string message)
    {
        return new Result { Succeeded = true, Message = message };
    }

    public static IResult Fail()
    {
        return new Result { Succeeded = false };
    }

    public static IResult Fail(string message)
    {
        return new Result { Succeeded = false, Message = message };
    }

    public static IResult Fail(List<string> errors)
    {
        return new Result { Succeeded = false, Errors = errors };
    }

    public static IResult Fail(string message, List<string> errors)
    {
        return new Result { Succeeded = false, Message = message, Errors = errors };
    }

    public static Task<IResult> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<IResult> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<IResult> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public static Task<IResult> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public static Task<IResult> FailAsync(List<string> errors)
    {
        return Task.FromResult(Fail(errors));
    }

    public static Task<IResult> FailAsync(string message, List<string> errors)
    {
        return Task.FromResult(Fail(message, errors));
    }
}

public class Result<T> : Result, IResult<T>
{
    public T Data { get; set; }

    public Result() { Succeeded = false; Message = string.Empty; Errors = new List<string>(); }

    public static new Result<T> Success()
    {
        return new Result<T> { Succeeded = true };
    }

    public static new Result<T> Success(string message)
    {
        return new Result<T> { Succeeded = true, Message = message };
    }

    public static Result<T> Success(T data)
    {
        return new Result<T> { Succeeded = true, Data = data };
    }

    public static Result<T> Success(T data, string message)
    {
        return new Result<T> { Succeeded = true, Data = data, Message = message };
    }

    public static new Task<Result<T>> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static new Task<Result<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<Result<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<Result<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }

    public static new Result<T> Fail()
    {
        return new Result<T> { Succeeded = false };
    }

    public static new Result<T> Fail(string message)
    {
        return new Result<T> { Succeeded = false, Message = message };
    }

    public static new Result<T> Fail(List<string> errors)
    {
        return new Result<T> { Succeeded = false, Errors = errors };
    }

    public static new Result<T> Fail(string message, List<string> errors)
    {
        return new Result<T> { Succeeded = false, Message = message, Errors = errors };
    }

    public static new Task<Result<T>> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public static new Task<Result<T>> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public static new Task<Result<T>> FailAsync(List<string> errors)
    {
        return Task.FromResult(Fail(errors));
    }

    public static new Task<Result<T>> FailAsync(string message, List<string> errors)
    {
        return Task.FromResult(Fail(message, errors));
    }
}
