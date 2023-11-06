using CodventureV1.Application.Common.Commands;
using CodventureV1.Application.Common.Commands.Update;
using CodventureV1.Application.Common.Models;
using CodventureV1.Application.Common.Queries;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Infrastructure.Repositories.Queries.Collections;
using CodventureV1.Presentation.Common.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodventureV1.Presentation.Common.Extensions;

public static class EntityGroupExtesions
{
    private const string GetRoute = "";
    private const string GetByIdRoute = "{id}";
    private const string PostRoute = "";
    private const string PutRoute = "{id}";
    private const string DeleteByIdRoute = "{id}";
    private const string DeleteRange = "";
    public static RouteGroupBuilder MapEntityGet<TQuery, TDto>(this RouteGroupBuilder group)
        where TDto : class
        where TQuery : class, IQuery<IPagedList<TDto>>
    {
        group.MapGet(GetRoute, Get<TQuery, TDto>);
        return group;
    }

    public static RouteGroupBuilder MapEntityGetById<TQuery, TDto>(this RouteGroupBuilder group)
        where TDto : class
        where TQuery : class, IQuery<TDto>
    {
        group.MapGet(GetByIdRoute, GetById<TQuery, TDto>);
        return group;
    }

    public static RouteGroupBuilder MapEntityPost<TKey, TCommand>(this RouteGroupBuilder group)
        where TCommand : class, ICommand<TKey>
    {
        group.MapPost(PostRoute, Post<TKey, TCommand>);
        return group;
    }

    public static RouteGroupBuilder MapEntityPostWithEntity<TKey, TCommand, TQuery, TDto>(this RouteGroupBuilder group)
        where TCommand : class, ICommand<TKey>
        where TQuery : class, IQuery<TDto>
    {
        group.MapPost(PostRoute, PostWithEntity<TKey, TCommand, TQuery, TDto>);
        return group;
    }
    public static RouteGroupBuilder MapEntityPut<TKey, TCommand>(this RouteGroupBuilder group)
        where TCommand : UpdateCommand<TKey>
    {
        group.MapPut(PutRoute, Put<TKey, TCommand>);
        return group;
    }

    private static async Task<Results<
        Ok<IResult<IPagedList<TDto>>>,
        BadRequest<ProblemDetails>>>
        Get<TGetQuery, TDto>(ISender sender, [AsParameters] PaginationParams @params)
        where TDto : class
        where TGetQuery : class, IQuery<IPagedList<TDto>>
    {
        var query = Activator.CreateInstance(typeof(TGetQuery), @params) as TGetQuery
            ?? throw new InvalidOperationException("Cannot create query instance");
        return ResultHandlers.HandleGetResult(await sender.Send(query));
    }

    private static async Task<Results<
        Ok<IResult<TDto>>,
        NotFound<ProblemDetails>>>
        GetById<TGetQuery, TDto>(ISender sender, [FromRoute] int id)
        where TDto : class
        where TGetQuery : class, IQuery<TDto>
    {
        var query = Activator.CreateInstance(typeof(TGetQuery), id) as TGetQuery
            ?? throw new InvalidOperationException("Cannot create query instance");
        return ResultHandlers.HandleGetByIdResult(await sender.Send(query));
    }

    private static async Task<Results<
        Ok<IResult<TKey>>,
        BadRequest<ProblemDetails>>>
        Post<TKey, TCommand>(ISender sender, [FromBody] TCommand request)
        where TCommand : class, ICommand<TKey>
        => ResultHandlers.HandlePostResult(await sender.Send(request));

    private static async Task<Results<
        Ok<IResult<TDto>>,
        BadRequest<ProblemDetails>>>
        PostWithEntity<TKey, TCommand, TQuery, TDto>(ISender sender, [FromBody] TCommand request)
        where TCommand : class, ICommand<TKey>
        where TQuery : class, IQuery<TDto>
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot create query instance");
        }
        var query = Activator.CreateInstance(typeof(TQuery), result.Value) as TQuery
            ?? throw new InvalidOperationException("Cannot create query instance");
        return ResultHandlers.HandlePostResult(await sender.Send(query));
    }

    private static async Task<Results<
        NoContent,
        NotFound<ProblemDetails>,
        BadRequest<ProblemDetails>
        >>
        Put<TKey, TCommand>(ISender sender, TKey id, [FromBody] TCommand request)
        where TCommand : UpdateCommand<TKey>
    {
        if (id is null || request.Id is null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Please provide the Id"
            };
            return TypedResults.BadRequest(problemDetails);
        }
        if (!id.Equals(request.Id))
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Ids not match"
            };
            return TypedResults.BadRequest(problemDetails);
        }
        return ResultHandlers.HandlePutResult(await sender.Send(request));
    }
}
