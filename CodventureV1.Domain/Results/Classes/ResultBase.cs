using CodventureV1.Domain.Results.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Domain.Results.Classes;

public abstract class ResultBase : IResultBase
{
    public bool IsFailed => Errors.Any();
    public bool IsSuccess => !IsFailed;
    public IDictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

    public int StatusCode { get; set; } = StatusCodes.Status200OK;
}

public abstract class ResultBase<TResult> : ResultBase
    where TResult : ResultBase<TResult>
{
    public TResult WithError(string key, string errorMessage)
    {
        if (Errors.TryGetValue(key, out var errors))
        {
            errors.Add(errorMessage);
        }
        else
        {
            Errors.Add(key, new List<string> { errorMessage });
        }
        return (TResult)this;
    }

    public TResult WithErrors(string key, IEnumerable<string> errorMessages)
    {
        if (Errors.TryGetValue(key, out var errors))
        {
            errors.AddRange(errorMessages);
        }
        else
        {
            Errors.Add(key, errorMessages.ToList());
        }
        return (TResult)this;
    }
    public TResult WithError(string errorMessage)
        => WithError(ResultConstants.DefaultErrorKey, errorMessage);

    public TResult WithErrors(IEnumerable<string> errorMessages)
        => WithErrors(ResultConstants.DefaultErrorKey, errorMessages);

    public TResult WithStatusCode(int statusCode)
    {
        StatusCode = statusCode;
        return (TResult)this;
    }
}