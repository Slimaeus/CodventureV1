using AutoMapper;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Infrastructure.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Common.Commands.Update;

public abstract record UpdateCommand<TKey>(TKey Id) : ICommand<Unit>;
public abstract class UpdateCommandHandler<TKey, TCommand, TEntity>(IUnitOfWork unitOfWork, IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper) : ICommandHandler<TCommand, Unit>
    where TCommand : UpdateCommand<TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly IQueryRepository<TKey, TEntity> _queryRepository = queryRepositoryFactory.CreateQueryRepository<TKey, TEntity>();

    public async Task<IResult<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await _queryRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return Result.Fail<Unit>("Not Found", StatusCodes.Status404NotFound);
        }

        mapper.Map(request, entity);

        await unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Result.Ok(Unit.Value);
    }
}

public abstract class UpdateProfile<TKey, TCommand, TEntity> : Profile
{
    public UpdateProfile()
        => CreateMap<TCommand, TEntity>()
        .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
}