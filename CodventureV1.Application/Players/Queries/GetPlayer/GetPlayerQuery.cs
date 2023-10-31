using CodventureV1.Application.Common.Queries;
using CodventureV1.Domain.Players;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Players.Queries.GetPlayer;

public sealed record GetPlayerQuery(Guid Id) : IQuery<Player>;

public sealed class Handler : IQueryHandler<GetPlayerQuery, Player>
{
    private readonly IPlayerQueryRepository _playerRepository;

    public Handler(IPlayerQueryRepository playerRepository)
        => _playerRepository = playerRepository;

    public async Task<IResult<Player>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.Id);
        if (player is null)
        {
            var error = "Player not found";
            return Result.Fail<Player>(error, StatusCodes.Status404NotFound);
        }
        return Result.Ok(player);
    }
}
