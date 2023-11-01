using CodventureV1.Application.Common.Queries;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Players;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Players.Queries.GetPlayer;

public sealed record GetPlayerQuery(Guid Id) : IQuery<Player>;

public sealed class Handler : IQueryHandler<GetPlayerQuery, Player>
{
    private readonly IQueryRepository<Guid, Player> _repository;

    public Handler(IQueryRepositoryFactory queryRepositoryFactory)
        => _repository = queryRepositoryFactory.CreateQueryRepository<Guid, Player>();

    public async Task<IResult<Player>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id);
        if (player is null)
        {
            var error = "Player not found";
            return Result.Fail<Player>(error, StatusCodes.Status404NotFound);
        }
        return Result.Ok(player);
    }
}
