namespace CodventureV1.Domain.Results.Interfaces;

public interface IResultBase
{
    bool IsFailed { get; }
    bool IsSuccess { get; }
    IDictionary<string, List<string>> Errors { get; }
    int StatusCode { get; }
}