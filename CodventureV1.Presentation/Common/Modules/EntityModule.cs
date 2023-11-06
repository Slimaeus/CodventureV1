using Carter;
using CodventureV1.Application.Common.Commands;
using CodventureV1.Application.Common.Queries;
using CodventureV1.Domain.Common.Extensions;
using CodventureV1.Infrastructure.Repositories.Queries.Collections;
using CodventureV1.Presentation.Common.Extensions;

namespace CodventureV1.Presentation.Common.Modules;

public abstract class EntityModule<TKey, TEntity, TDto> : ICarterModule
    where TDto : class
{
    protected RouteGroupBuilder Group { get; set; } = default!;
    protected double Version { get; set; } = 0;
    public virtual void AddRoutes(IEndpointRouteBuilder app)
    {
        var entityName = typeof(TEntity).Name;
        var apiGroup = app.MapGroup("api");

        if (Version != 0)
            apiGroup = apiGroup.MapGroup($"v{Version}");

        Group = apiGroup.MapGroup($"{entityName.Pluralize()}")
            .WithTags(entityName);
    }

    protected void MapGetWithPagination<TQuery>()
        where TQuery : class, IQuery<IPagedList<TDto>>
    {
        Group.MapEntityGet<TQuery, TDto>();
    }

    protected void MapGetById<TQuery>()
        where TQuery : class, IQuery<TDto>
    {
        Group.MapEntityGetById<TQuery, TDto>();
    }

    protected void MapPost<TCommand>()
        where TCommand : class, ICommand<TKey>
    {
        Group.MapEntityPost<TKey, TCommand>();
    }

    protected void MapPostWithEntity<TCommand, TQuery>()
        where TCommand : class, ICommand<TKey>
        where TQuery : class, IQuery<TDto>
    {
        Group.MapEntityPostWithEntity<TKey, TCommand, TQuery, TDto>();
    }
}