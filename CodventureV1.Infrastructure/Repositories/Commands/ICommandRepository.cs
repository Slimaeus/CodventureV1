using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CodventureV1.Infrastructure.Repositories.Commands;
public interface ICommandRepository<TEntity> where TEntity : class
{
    TEntity Add(TEntity entity);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    int Remove(Expression<Func<TEntity, bool>> predicate);
    TEntity Remove(TEntity entity);
    Task<int> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    void RemoveRange(IEnumerable<TEntity> entities);
    int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> expression);
    TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
    Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> expression, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] properties);
}