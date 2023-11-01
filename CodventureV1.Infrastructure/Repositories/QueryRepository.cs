using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodventureV1.Infrastructure.Repositories;

public class QueryRepository<TKey, TEntity> : IQueryRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public QueryRepository(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;

    public Task<IQueryable<TEntity>> GetAsync(ISpecification specification)
    {
        var query = _applicationDbContext.Set<TEntity>()
            .OrderBy(x => x.Id)
            .AsSplitQuery();

        if (specification is not null && specification.PageSize > 0)
        {
            var takeCount = specification.PageSize;
            var skip = (specification.PageIndex - 1) * specification.PageSize;
            query = query
                .Take(takeCount)
                .Skip(skip);
        }

        return Task.FromResult(query);
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _applicationDbContext.Set<TEntity>()
            .FindAsync(id);
}
