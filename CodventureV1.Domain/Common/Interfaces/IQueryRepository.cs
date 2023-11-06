using System.Linq.Expressions;

namespace CodventureV1.Domain.Common.Interfaces;
public interface IQueryRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
{
    Task<IQueryable<TEntity>> GetAsync(ISpecification<TEntity> specification = default!);
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = default!, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = default!, CancellationToken cancellationToken = default);
}