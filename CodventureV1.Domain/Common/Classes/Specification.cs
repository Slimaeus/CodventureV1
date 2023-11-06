using CodventureV1.Domain.Common.Interfaces;
using System.Linq.Expressions;

namespace CodventureV1.Domain.Common.Classes;

public record Specification<TItem>(int PageIndex = 1, int PageSize = 10, Expression<Func<TItem, bool>> Predicate = default!) : ISpecification<TItem>
    where TItem : class;
