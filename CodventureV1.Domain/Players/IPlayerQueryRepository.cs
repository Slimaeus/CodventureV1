using CodventureV1.Domain.Common.Interfaces;

namespace CodventureV1.Domain.Players;

public interface IPlayerQueryRepository : IQueryRepository<Guid, Player>
{
}