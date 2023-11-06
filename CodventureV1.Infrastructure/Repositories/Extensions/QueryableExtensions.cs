using CodventureV1.Domain.Common.Interfaces;
using System.Linq.Expressions;

namespace CodventureV1.Infrastructure.Repositories.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate) where T : class
    {
        if (predicate is null)
        {
            return source;
        }

        return source.Where(predicate);
    }
    public static IQueryable<T> Page<T>(this IQueryable<T> source, ISpecification specification) where T : class
    {
        if (!(specification?.PageSize > 0))
        {
            return source;
        }

        var skipCount = (specification.PageIndex - 1) * specification.PageSize;

        return skipCount < 0 ? source : source.Skip(skipCount).Take(specification.PageSize);
    }
}
