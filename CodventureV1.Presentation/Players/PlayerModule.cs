using CodventureV1.Application.Players.Dtos;
using CodventureV1.Application.Players.Queries.GetPlayer;
using CodventureV1.Application.Players.Queries.GetPlayers;
using CodventureV1.Domain.Players;
using CodventureV1.Presentation.Common.Modules;

namespace CodventureV1.Presentation.Players;

public sealed class PlayerModule : EntityModule<Guid, Player, PlayerDto>
{
    public PlayerModule()
        => Version = 1;
    protected override void MapEndpoints()
    {
        MapGetWithPagination<GetPlayersQuery>();
        MapGetById<GetPlayerQuery>();
    }
}
