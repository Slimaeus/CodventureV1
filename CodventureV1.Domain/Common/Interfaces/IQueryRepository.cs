namespace CodventureV1.Domain.Common.Interfaces;
public interface IQueryRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAsync(ISpecification specification = default!);
    Task<TEntity?> GetByIdAsync(TKey id);
}