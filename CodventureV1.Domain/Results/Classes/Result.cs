using CodventureV1.Domain.Results.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Domain.Results.Classes;

public sealed class Result<TValue> : ResultBase<Result<TValue>>, IResult<TValue>, IResultBase
{
    private TValue _value = default!;
    public TValue Value
    {
        get
        {
            return _value;
        }
        private set
        {
            _value = value;
        }
    }

    public Result<TValue> WithValue(TValue value)
    {
        Value = value;
        return this;
    }
}

public class Result : ResultBase<Result>
{
    public static Result Ok()
    {
        return new Result();
    }

    public static Result<TValue> Ok<TValue>(TValue value)
    {
        var result = new Result<TValue>();
        result.WithValue(value);
        return result;
    }

    public static Result Fail(string error, int statusCode = StatusCodes.Status400BadRequest)
    {
        var result = new Result();
        result.WithError(error);
        result.WithStatusCode(statusCode);
        return result;
    }

    public static Result Fail(IEnumerable<string> errors, int statusCode = StatusCodes.Status400BadRequest)
    {
        var result = new Result();
        result.WithErrors(errors);
        result.WithStatusCode(statusCode);
        return result;
    }

    public static Result<TValue> Fail<TValue>(string error, int statusCode = StatusCodes.Status400BadRequest)
    {
        var result = new Result<TValue>();
        result.WithError(error);
        result.WithStatusCode(statusCode);
        return result;
    }

    public static Result<TValue> Fail<TValue>(IEnumerable<string> errors, int statusCode = StatusCodes.Status400BadRequest)
    {
        var result = new Result<TValue>();
        result.WithErrors(errors);
        result.WithStatusCode(statusCode);
        return result;
    }
}