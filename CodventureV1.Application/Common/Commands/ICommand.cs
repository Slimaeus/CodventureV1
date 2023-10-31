using CodventureV1.Domain.Results.Interfaces;
using MediatR;

namespace CodventureV1.Application.Common.Commands;

public interface ICommand<TValue> : IRequest<IResult<TValue>> { }
