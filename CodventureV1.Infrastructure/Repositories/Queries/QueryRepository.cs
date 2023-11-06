using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Infrastructure.Repositories.Queries.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodventureV1.Infrastructure.Repositories.Queries;

public class QueryRepository<TKey, TEntity> : IQueryRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
{
    private readonly DbSet<TEntity> _dbSet;

    public QueryRepository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    public Task<IQueryable<TEntity>> GetAsync(ISpecification<TEntity> specification)
    {
        var query = _dbSet
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
        => await _dbSet
            .FindAsync(id);

    public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = default!, CancellationToken cancellationToken = default)
    {
        var result = predicate == null ? _dbSet.AnyAsync(cancellationToken) : _dbSet.AnyAsync(predicate, cancellationToken);
        return result;
    }

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = default!, CancellationToken cancellationToken = default)
    {
        var result = predicate == null ? _dbSet.CountAsync(cancellationToken) : _dbSet.CountAsync(predicate, cancellationToken);

        return result;
    }
}
