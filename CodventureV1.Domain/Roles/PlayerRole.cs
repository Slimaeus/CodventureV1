using CodventureV1.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CodventureV1.Domain.Roles;

public class PlayerRole : IdentityRole<Guid>, IEntity<Guid>
{
}
