using CodventureV1.Infrastructure.Repositories.Commands;
using CodventureV1.Persistence;

namespace CodventureV1.Infrastructure.UnitOfWorks;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ICommandRepositoryFactory _commandRepositoryFactory;

    public UnitOfWork(ApplicationDbContext applicationDbContext, ICommandRepositoryFactory commandRepositoryFactory)
    {
        _applicationDbContext = applicationDbContext;
        _commandRepositoryFactory = commandRepositoryFactory;
    }

    public ICommandRepository<TEntity> Repository<TEntity>() where TEntity : class
        => _commandRepositoryFactory.CreateCommandRepository<TEntity>();

    public bool HasChanges()
    {
        bool autoDetectChangesEnabled;

        if (!(autoDetectChangesEnabled = _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled))
        {
            _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        try
        {
            var hasChanges = _applicationDbContext.ChangeTracker.HasChanges();

            return hasChanges;
        }
        finally
        {
            _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;
        }
    }


    public int SaveChanges(bool acceptAllChangesOnSuccess = true)
    {
        if (!HasChanges())
        {
            return 0;
        }

        bool autoDetectChangesEnabled;

        if (!(autoDetectChangesEnabled = _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled))
        {
            _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        try
        {
            return _applicationDbContext.SaveChanges(acceptAllChangesOnSuccess);
        }
        finally
        {
            _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;
        }
    }

    public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default)
    {
        if (!HasChanges())
        {
            return await Task.FromResult(0).ConfigureAwait(continueOnCapturedContext: false);
        }

        bool autoDetectChangesEnabled;

        if (!(autoDetectChangesEnabled = _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled))
        {
            _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        try
        {
            return await _applicationDbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        }
        finally
        {
            _applicationDbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;
        }
    }
}
