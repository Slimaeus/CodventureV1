using CodventureV1.Domain.Results.Interfaces;

namespace CodventureV1.Domain.Results.Extensions;

public static class ResultExtensions
{
    public static TValue Match<TResult, TValue>(
        this TResult result,
        Func<int, TValue> onSuccess,
        Func<int, IDictionary<string, List<string>>, TValue> onFailure)
        where TResult : IResultBase
    {
        return result.IsSuccess ? onSuccess(result.StatusCode) : onFailure(result.StatusCode, result.Errors);
    }
}
