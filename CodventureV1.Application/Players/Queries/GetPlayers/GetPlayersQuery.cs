using AutoMapper;
using CodventureV1.Application.Common.Models;
using CodventureV1.Application.Common.Queries.GetWithPagination;
using CodventureV1.Application.Players.Dtos;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Players;

namespace CodventureV1.Application.Players.Queries.GetPlayers;

//public sealed record GetPlayersQuery : IQuery<IEnumerable<Player>>;

//public sealed class Handler : IQueryHandler<GetPlayersQuery, IEnumerable<Player>>
//{
//    private readonly IPlayerQueryRepository _playerRepository;

//    public Handler(IPlayerQueryRepository playerRepository)
//        => _playerRepository = playerRepository;

//    public async Task<IResult<IEnumerable<Player>>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
//    {
//        return Result.Ok(await _playerRepository.GetAsync());
//    }
//}

public sealed record GetPlayersQuery(IPaginationParams Params) : GetWithPaginationQuery<PlayerDto>(Params);

public sealed class Handler(IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper)
    : GetWithPaginationHandler<Guid, Player, GetPlayersQuery, PlayerDto>(queryRepositoryFactory, mapper)
{
}
