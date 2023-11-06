namespace CodventureV1.Infrastructure.Repositories.Commands;
public interface ICommandRepositoryFactory
{
    ICommandRepository<TEntity> CreateCommandRepository<TEntity>() where TEntity : class;
}