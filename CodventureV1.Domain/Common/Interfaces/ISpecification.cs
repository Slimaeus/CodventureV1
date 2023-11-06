using System.Linq.Expressions;

namespace CodventureV1.Domain.Common.Interfaces;

public interface ISpecification<TItem>
    where TItem : class
{
    int PageIndex { get; }
    int PageSize { get; }
    Expression<Func<TItem, bool>> Predicate { get; }
}