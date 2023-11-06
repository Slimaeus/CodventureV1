using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Infrastructure.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CodventureV1.Infrastructure.Repositories;

public class QueryRepository<TKey, TEntity> : IQueryRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
{
    private readonly DbContext _dbContext;

    public QueryRepository(DbContext dbContext)
        => _dbContext = dbContext;

    public Task<IQueryable<TEntity>> GetAsync(ISpecification<TEntity> specification)
    {
        var query = _dbContext
            .Set<TEntity>()
            .OrderBy(x => x.Id)
            .AsSplitQuery();

        if (specification is null)
        {
            return Task.FromResult(query);
        }

        query = query.Filter(specification.Predicate);

        query = query.Page(specification);

        return Task.FromResult(query);
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _dbContext.Set<TEntity>()
            .FindAsync(id);
}
