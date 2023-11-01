using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;

namespace CodventureV1.Application.Common.Queries.GetWithPagination;

public abstract record GetWithPaginationQuery<TItem>(ISpecification Specification) : IQuery<IEnumerable<TItem>>
    where TItem : class;

public abstract class GetWithPaginationHandler<TKey, TEntity, TQuery, TItem> : IQueryHandler<TQuery, IEnumerable<TItem>>
    where TQuery : GetWithPaginationQuery<TItem>
    where TEntity : class, IEntity<TKey>
    where TItem : class
{
    private readonly IQueryRepository<TKey, TEntity> _repository;
    private readonly IMapper _mapper;

    public GetWithPaginationHandler(IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper)
    {
        _repository = queryRepositoryFactory.CreateQueryRepository<TKey, TEntity>();
        _mapper = mapper;
    }

    public async Task<IResult<IEnumerable<TItem>>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var players = await _repository.GetAsync(request.Specification);
        var playerDtos = players
            .ProjectTo<TItem>(_mapper.ConfigurationProvider);
        return Result.Ok(playerDtos);
    }
}
