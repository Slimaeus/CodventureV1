using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;

namespace CodventureV1.Application.Common.Queries.GetWithPagination;

public abstract record GetWithPaginationQuery<TItem>(ISpecification Specification) : IQuery<IEnumerable<TItem>>
    where TItem : class;

public abstract class GetWithPaginationHandler<TKey, TEntity, TQuery> : IQueryHandler<TQuery, IEnumerable<TEntity>>
    where TQuery : GetWithPaginationQuery<TEntity>
    where TEntity : class, IEntity<TKey>
{
    private readonly IQueryRepository<TKey, TEntity> _repository;

    public GetWithPaginationHandler(IQueryRepositoryFactory queryRepositoryFactory)
        => _repository = queryRepositoryFactory.CreateQueryRepository<TKey, TEntity>();
    public async Task<IResult<IEnumerable<TEntity>>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        return Result.Ok(await _repository.GetAsync(request.Specification));
    }
}
