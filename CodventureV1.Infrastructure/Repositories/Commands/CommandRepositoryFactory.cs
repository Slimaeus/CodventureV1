using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodventureV1.Infrastructure.Repositories.Commands;

public sealed class CommandRepositoryFactory : ICommandRepositoryFactory
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CommandRepositoryFactory(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;

    public ICommandRepository<TEntity> CreateCommandRepository<TKey, TEntity>() where TEntity : class, IEntity<TKey>
    {
        var repositoryType = typeof(CommandRepository<,>).MakeGenericType(typeof(TKey), typeof(TEntity));

        var constructor = repositoryType.GetConstructor(new[] { typeof(DbContext) });

        if (constructor is { })
        {
            return (ICommandRepository<TEntity>)constructor.Invoke(new object[] { _applicationDbContext });
        }

        throw new InvalidOperationException($"No suitable constructor found for {repositoryType.Name}.");
    }
}
