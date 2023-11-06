using CodventureV1.Application.Common.Commands;
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
    private const string UpdateRoute = "";
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

    public static RouteGroupBuilder MapEntityPost<TCommand, TKey>(this RouteGroupBuilder group)
        where TCommand : class, ICommand<TKey>
    {
        group.MapPost(PostRoute, Post<TCommand, TKey>);
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
        Post<TCommand, TKey>(ISender sender, [FromBody] TCommand request)
        where TCommand : class, ICommand<TKey>
        => ResultHandlers.HandlePostResult(await sender.Send(request));
}
