using AutoMapper;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Persistence;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Common.Queries.GetById;

public abstract record GetByIdQuery<TKey, TItem>(TKey Id) : IQuery<TItem>;
public abstract class GetByIdQueryHandler<TKey, TQuery, TEntity, TItem>(IQueryRepositoryFactory queryRepositoryFactory, ApplicationDbContext applicationDbContext, IMapper mapper) : IQueryHandler<TQuery, TItem>
    where TQuery : GetByIdQuery<TKey, TItem>
    where TEntity : class, IEntity<TKey>
    where TItem : class
{
    private readonly IQueryRepository<TKey, TEntity> _repository = queryRepositoryFactory.CreateQueryRepository<TKey, TEntity>();

    public async Task<IResult<TItem>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id);

        if (player is null)
        {
            var error = "Not Found";
            return Result.Fail<TItem>(error, StatusCodes.Status404NotFound);
        }
        foreach (var item in applicationDbContext.Entry(player).References)
        {
            await item.LoadAsync(cancellationToken);
        };
        var playerDto = mapper.Map<TItem>(player);
        return Result.Ok(playerDto);
    }
}
