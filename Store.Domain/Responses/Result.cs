namespace Store.Domain.Responses;

public class Result
{
    public bool IsSuccess { get; }
    public string[] Errors { get; }
    public object Data { get; }

    private Result(bool isSuccess, string[] errors, object data)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        Data = data;
    }

    public static Result Success(object data)
    {
        return new Result(true, Array.Empty<string>(), data);
    }

    public static Result Failure(string[] errors)
    {
        return new Result(false, errors, null);
    }
}