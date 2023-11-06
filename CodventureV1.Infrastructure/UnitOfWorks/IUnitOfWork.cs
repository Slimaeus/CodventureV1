using CodventureV1.Infrastructure.Repositories.Commands;

namespace CodventureV1.Infrastructure.UnitOfWorks;
public interface IUnitOfWork
{
    bool HasChanges();
    ICommandRepository<TEntity> Repository<TEntity>() where TEntity : class;
    int SaveChanges(bool acceptAllChangesOnSuccess = true);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default);
}