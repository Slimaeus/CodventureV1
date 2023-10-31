using CodventureV1.Domain.Results.Interfaces;
using MediatR;

namespace CodventureV1.Application.Common.Queries;

public interface IQuery<TValue> : IRequest<IResult<TValue>> { }
public interface IQueryHandler<TQuery, TValue> : IRequestHandler<TQuery, IResult<TValue>>
    where TQuery : IQuery<TValue>
{ }
