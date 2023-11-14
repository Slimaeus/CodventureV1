using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodventureV1.Application.Common.Models;
using CodventureV1.Domain.Common.Classes;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Infrastructure.Repositories.Queries.Collections;
using CodventureV1.Infrastructure.Repositories.Queries.Extensions;

namespace CodventureV1.Application.Common.Queries.GetWithPagination;

public abstract record GetWithPaginationQuery<TItem>(IPaginationParams Params) : IQuery<IPagedList<TItem>>
    where TItem : class;

public abstract class GetWithPaginationHandler<TKey, TEntity, TQuery, TItem>(IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper) : IQueryHandler<TQuery, IPagedList<TItem>>
    where TQuery : GetWithPaginationQuery<TItem>
    where TEntity : class, IEntity<TKey>
    where TItem : class
{
    private readonly IQueryRepository<TKey, TEntity> _repository = queryRepositoryFactory.CreateQueryRepository<TKey, TEntity>();

    public async Task<IResult<IPagedList<TItem>>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var specification = mapper.Map<Specification<TEntity>>(request.Params);
        var players = await _repository.GetAsync(specification);
        var playerDtos = players
            .ProjectTo<TItem>(mapper.ConfigurationProvider)
            .ToPagedList(specification.PageIndex, specification.PageSize, players.Count());
        return Result.Ok(playerDtos);
    }
}
