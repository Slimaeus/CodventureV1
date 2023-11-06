using CodventureV1.Domain.Results.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodventureV1.Presentation.Common.Handlers;

public static class ResultHandlers
{
    public static Results<Ok<TResult>, NotFound<ProblemDetails>> HandleGetByIdResult<TResult>(TResult result)
        where TResult : IResultBase
        => result.StatusCode switch
        {
            StatusCodes.Status200OK => TypedResults.Ok(result),
            StatusCodes.Status404NotFound => TypedResults.NotFound(result.ToProblemDetails()),
            _ => throw new Exception(),
        };

    public static Results<Ok<TResult>, BadRequest<ProblemDetails>> HandleGetResult<TResult>(TResult result)
        where TResult : IResultBase
        => result.StatusCode switch
        {
            StatusCodes.Status200OK => TypedResults.Ok(result),
            StatusCodes.Status400BadRequest => TypedResults.BadRequest(result.ToProblemDetails()),
            _ => throw new Exception()
        };
    public static Results<Ok<TResult>, BadRequest<ProblemDetails>> HandlePostResult<TResult>(TResult result)
        where TResult : IResultBase
        => result.StatusCode switch
        {
            StatusCodes.Status200OK => TypedResults.Ok(result),
            StatusCodes.Status400BadRequest => TypedResults.BadRequest(result.ToProblemDetails()),
            _ => throw new Exception()
        };
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
                ["errors"] = result.Errors
            },
        };
}
