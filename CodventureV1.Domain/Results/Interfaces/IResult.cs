namespace CodventureV1.Domain.Results.Interfaces;

public interface IResult<out TValue> : IResultBase
{
    TValue Value { get; }
}