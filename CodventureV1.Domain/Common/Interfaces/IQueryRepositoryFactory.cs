namespace CodventureV1.Domain.Common.Interfaces;
public interface IQueryRepositoryFactory
{
    IQueryRepository<TKey, TEntity> CreateQueryRepository<TKey, TEntity>() where TEntity : class, IEntity<TKey>;
}