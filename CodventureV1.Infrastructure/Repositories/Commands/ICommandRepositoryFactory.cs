using CodventureV1.Domain.Common.Interfaces;

namespace CodventureV1.Infrastructure.Repositories.Commands;
public interface ICommandRepositoryFactory
{
    ICommandRepository<TEntity> CreateCommandRepository<TKey, TEntity>() where TEntity : class, IEntity<TKey>;
}