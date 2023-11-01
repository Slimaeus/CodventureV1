using AutoMapper;
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

public sealed record GetPlayersQuery(ISpecification Specification) : GetWithPaginationQuery<PlayerDto>(Specification);

public sealed class Handler : GetWithPaginationHandler<Guid, Player, GetPlayersQuery, PlayerDto>
{
    public Handler(IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper) : base(queryRepositoryFactory, mapper)
    {
    }
}
