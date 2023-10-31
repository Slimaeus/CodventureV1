using CodventureV1.Application.Common.Queries.GetWithPagination;
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

public sealed record GetPlayersQuery(ISpecification Specification) : GetWithPaginationQuery<Player>(Specification);

public sealed class Handler : GetWithPaginationHandler<Guid, Player, GetPlayersQuery>
{
    public Handler(IQueryRepositoryFactory queryRepositoryFactory) : base(queryRepositoryFactory)
    {
    }
}
