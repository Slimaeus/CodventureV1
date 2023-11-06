using CodventureV1.Infrastructure.Repositories.Queries.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CodventureV1.Infrastructure.Repositories.Commands;

public class CommandRepository<TKey, TEntity> : ICommandRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public CommandRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    public virtual TEntity Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} cannot be null.");
        }

        _dbSet.Add(entity);

        return entity;
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} cannot be null.");
        }

        if (!entities.Any())
        {
            return;
        }

        _dbSet.AddRange(entities);
    }
    public virtual int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> expression)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} cannot be null.");
        }

        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression), $"{nameof(expression)} cannot be null.");
        }

        var result = _dbSet.Where(predicate).ExecuteUpdate(expression);

        return result;
    }
    public virtual TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} cannot be null.");
        }

        if (properties?.Any() ?? false)
        {
            var entityEntry = _dbContext.Entry(entity);

            foreach (var property in properties)
            {
                PropertyEntry propertyEntry;

                try
                {
                    propertyEntry = entityEntry.Property(property);
                }
                catch { propertyEntry = null; }

                if (propertyEntry != null)
                {
                    propertyEntry.IsModified = true;
                }
                else
                {
                    ReferenceEntry referenceEntry;

                    try
                    {
                        referenceEntry = entityEntry.Reference(property);
                    }
                    catch { referenceEntry = null; }

                    if (referenceEntry != null)
                    {
                        var referenceEntityEntry = referenceEntry.TargetEntry;

                        _dbContext.Update(referenceEntityEntry.Entity);
                    }
                }
            }
        }
        else
        {
            _dbSet.Update(entity);
        }

        return entity;
    }
    public virtual void UpdateRange(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] properties)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} cannot be null.");
        }

        if (!entities.Any())
        {
            return;
        }

        if (properties?.Any() ?? false)
        {
            foreach (var entity in entities)
            {
                Update(entity, properties);
            }
        }
        else
        {
            _dbSet.UpdateRange(entities);
        }
    }
    public virtual TEntity Remove(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} cannot be null.");

        _dbSet.Remove(entity);

        return entity;
    }

    public virtual int Remove(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} cannot be null.");
        }

        var result = _dbSet.Where(predicate).ExecuteDelete();

        return result;
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} cannot be null.");
        }

        if (!entities.Any())
        {
            return;
        }

        _dbSet.RemoveRange(entities);
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} cannot be null.");
        }

        var result = _dbSet.AddAsync(entity, cancellationToken).AsTask().Then(result => result.Entity, cancellationToken);

        return result;
    }

    public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} cannot be null.");
        }

        if (!entities.Any())
        {
            return Task.CompletedTask;
        }

        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> expression, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} cannot be null.");
        }

        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression), $"{nameof(expression)} cannot be null.");
        }

        var result = _dbSet.Where(predicate).ExecuteUpdateAsync(expression, cancellationToken);

        return result;
    }

    public virtual Task<int> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} cannot be null.");
        }

        var result = _dbSet.Where(predicate).ExecuteDeleteAsync(cancellationToken);

        return result;
    }
}
