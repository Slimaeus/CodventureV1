using CodventureV1.Domain.Results.Interfaces;

namespace CodventureV1.Presentation.Common.Extensions;

public static class ResultExtensions
{
    public static TResult GetHttpResult<TResult>(this IResultBase result, Func<IResultBase, IResult>? onRest = null, string createdUri = "")
        where TResult : IResult
    {
        return (TResult)(result.StatusCode switch
        {
            StatusCodes.Status200OK => TypedResults.Ok(result),
            StatusCodes.Status201Created => TypedResults.Created(createdUri, result),
            StatusCodes.Status204NoContent => TypedResults.NoContent(),
            StatusCodes.Status400BadRequest => TypedResults.BadRequest(result.Errors),
            StatusCodes.Status404NotFound => TypedResults.NotFound(result.Errors),
            _ => onRest is null ? TypedResults.NoContent() : onRest(result),
        });
    }
}
