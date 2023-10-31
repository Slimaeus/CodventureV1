using CodventureV1.Domain.Players;
using CodventureV1.Infrastructure.Repositories;
using CodventureV1.Persistence;

namespace CodventureV1.Infrastructure.Players;

public sealed class PlayerQueryRepository : QueryRepository<Guid, Player>, IPlayerQueryRepository
{
    public PlayerQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}
