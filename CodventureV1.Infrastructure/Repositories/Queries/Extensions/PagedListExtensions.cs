using CodventureV1.Infrastructure.Repositories.Queries.Collections;

namespace CodventureV1.Infrastructure.Repositories.Queries.Extensions;

public static class PagedListExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int? pageIndex, int? pageSize, int totalCount)
        => new PagedList<T>(source, pageIndex, pageSize, totalCount);

    public static IPagedList<T> ToPagedList<T>(this IList<T> source, int? pageIndex, int? pageSize, int totalCount)
        => new PagedList<T>(source, pageIndex, pageSize, totalCount);

    public static Task<IPagedList<T>> ToPagedListAsync<T>(this Task<IList<T>> source, int? pageIndex, int? pageSize, int totalCount, CancellationToken cancellationToken = default)
        => source.Then<IList<T>, IPagedList<T>>(result => new PagedList<T>(result, pageIndex, pageSize, totalCount), cancellationToken);
}