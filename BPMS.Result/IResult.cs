namespace BPMS.Result;

public interface IResult
{
    bool Succeeded { get; set; }
    string Message { get; set; }
    public List<string> Errors { get; set; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}
