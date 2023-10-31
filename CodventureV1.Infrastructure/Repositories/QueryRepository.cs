using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodventureV1.Infrastructure.Repositories;

public class QueryRepository<TKey, TEntity> : IQueryRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public QueryRepository(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;

    public async Task<IEnumerable<TEntity>> GetAsync(ISpecification specification)
    {
        if (specification is null || specification.PageSize <= 0)
        {
            var queryWithoutTakeSkip = EF.CompileAsyncQuery((ApplicationDbContext context)
            => context.Set<TEntity>()
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .AsQueryable());

            return await queryWithoutTakeSkip(_applicationDbContext).ToListAsync();
        }

        var takeCount = specification.PageSize;
        var skip = (specification.PageIndex - 1) * specification.PageSize;
        var queryWithTakeSkip = EF.CompileAsyncQuery((ApplicationDbContext context, int take, int skip)
            => context.Set<TEntity>()
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Take(take)
            .Skip(skip));

        return await queryWithTakeSkip(_applicationDbContext, takeCount, skip).ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var query = EF.CompileAsyncQuery((ApplicationDbContext context, TKey id)
            => context.Set<TEntity>()
            .AsNoTracking()
            .SingleOrDefault(x => x.Id!.Equals(id)));

        return await query(_applicationDbContext, id);
    }
}
