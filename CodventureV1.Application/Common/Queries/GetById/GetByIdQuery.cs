using AutoMapper;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Persistence;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Common.Queries.GetById;

public abstract record GetByIdQuery<TKey, TItem>(TKey Id) : IQuery<TItem>;
public abstract class GetByIdQueryHandler<TKey, TQuery, TEntity, TItem> : IQueryHandler<TQuery, TItem>
    where TQuery : GetByIdQuery<TKey, TItem>
    where TEntity : class, IEntity<TKey>
    where TItem : class
{
    private readonly IQueryRepository<TKey, TEntity> _repository;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(IQueryRepositoryFactory queryRepositoryFactory, ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _repository = queryRepositoryFactory.CreateQueryRepository<TKey, TEntity>();
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<IResult<TItem>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id);

        if (player is null)
        {
            var error = "Not Found";
            return Result.Fail<TItem>(error, StatusCodes.Status404NotFound);
        }
        foreach (var item in _applicationDbContext.Entry(player).References)
        {
            await item.LoadAsync(cancellationToken);
        };
        var playerDto = _mapper.Map<TItem>(player);
        return Result.Ok(playerDto);
    }
}
