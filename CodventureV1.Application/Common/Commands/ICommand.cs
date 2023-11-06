using CodventureV1.Domain.Results.Interfaces;
using MediatR;

namespace CodventureV1.Application.Common.Commands;

public interface ICommand<TValue> : IRequest<IResult<TValue>> { }
public interface ICommandHandler<TCommand, TValue> : IRequestHandler<TCommand, IResult<TValue>>
    where TCommand : ICommand<TValue>
{ }
