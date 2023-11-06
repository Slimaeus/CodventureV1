using AutoMapper;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Infrastructure.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Common.Commands.Update;

public abstract record UpdateCommand<TKey>(TKey Id) : ICommand<Unit>;
public abstract class UpdateCommandHandler<TKey, TCommand, TEntity> : ICommandHandler<TCommand, Unit>
    where TCommand : UpdateCommand<TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQueryRepository<TKey, TEntity> _queryRepository;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(IUnitOfWork unitOfWork, IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _queryRepository = queryRepositoryFactory.CreateQueryRepository<TKey, TEntity>();
        _mapper = mapper;
    }
    public async Task<IResult<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await _queryRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return Result.Fail<Unit>("Not Found", StatusCodes.Status404NotFound);
        }

        _mapper.Map(request, entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Result.Ok(Unit.Value);
    }
}

public abstract class UpdateProfile<TKey, TCommand, TEntity> : Profile
{
    public UpdateProfile()
        => CreateMap<TCommand, TEntity>()
        .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
}