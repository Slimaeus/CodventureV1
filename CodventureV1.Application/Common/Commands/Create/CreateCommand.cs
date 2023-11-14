using AutoMapper;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Infrastructure.Repositories.Commands;
using CodventureV1.Infrastructure.UnitOfWorks;

namespace CodventureV1.Application.Common.Commands.Create;

public abstract record CreateCommand<TKey> : ICommand<TKey>;
public abstract class CreateCommandHandler<TCommand, TEntity, TKey>(IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<TCommand, TKey>
    where TCommand : CreateCommand<TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly ICommandRepository<TEntity> _repository = unitOfWork.Repository<TEntity>();
    public async Task<IResult<TKey>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var skill = mapper.Map<TEntity>(request);

        var result = await _repository.AddAsync(skill, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Result.Ok(result.Id);
    }
}
