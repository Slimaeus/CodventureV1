using CodventureV1.Domain.Players;
using CodventureV1.Domain.Roles;
using CodventureV1.Domain.Skills;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodventureV1.Persistence;

public sealed class ApplicationDbContext
    : IdentityDbContext<Player, PlayerRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillType> SkillTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
        => base.OnModelCreating(builder
            .ApplyConfigurationsFromAssembly(Persistence.AssemblyProvider.ExecutingAssembly));
}
