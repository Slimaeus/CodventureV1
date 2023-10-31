using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Persistence;

namespace CodventureV1.Infrastructure.Repositories;

public sealed class QueryRepositoryFactory : IQueryRepositoryFactory
{
    private readonly ApplicationDbContext _applicationDbContext;

    public QueryRepositoryFactory(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;

    public IQueryRepository<TKey, TEntity> CreateQueryRepository<TKey, TEntity>() where TEntity : class, IEntity<TKey>
    {
        var repositoryType = typeof(QueryRepository<,>).MakeGenericType(typeof(TKey), typeof(TEntity));

        var constructor = repositoryType.GetConstructor(new[] { typeof(ApplicationDbContext) });

        if (constructor is { })
        {
            return (IQueryRepository<TKey, TEntity>)constructor.Invoke(new object[] { _applicationDbContext });
        }
        throw new InvalidOperationException($"No suitable constructor found for {repositoryType.Name}.");
    }
}
