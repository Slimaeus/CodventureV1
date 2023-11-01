using CodventureV1.Domain.Results.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodventureV1.Presentation.Common.Extensions;

public static class ResultExtensions
{
    public static TResult ToHttpResult<TResult>(this IResultBase result, Func<IResultBase, IResult>? onRest = null, string createdUri = "")
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

    public static Results<Ok<TResult>, NotFound<ProblemDetails>> ToGetByIdResult<TResult>(this TResult result)
        where TResult : IResultBase
    {
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => TypedResults.Ok(result),
            StatusCodes.Status404NotFound => TypedResults.NotFound(result.ToProblemDetails()),
            _ => throw new Exception(),
        };
    }


    public static ProblemDetails ToProblemDetails(this IResultBase result)
        => result.StatusCode switch
        {
            StatusCodes.Status400BadRequest => result.CreateProblemDetails("Bad Request"),
            StatusCodes.Status404NotFound => result.CreateProblemDetails("Not Found"),
            _ => result.CreateProblemDetails("Unknow error")
        };
    public static ProblemDetails CreateProblemDetails(this IResultBase result, string title)
        => new()
        {
            Status = result.StatusCode,
            Title = title,
            Extensions =
                {
                        ["Errors"] = result.Errors
                },
        };
}
