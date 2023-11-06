﻿namespace CodventureV1.Domain.Common.Interfaces;
public interface IQueryRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
{
    Task<IQueryable<TEntity>> GetAsync(ISpecification<TEntity> specification = default!);
    Task<TEntity?> GetByIdAsync(TKey id);
}