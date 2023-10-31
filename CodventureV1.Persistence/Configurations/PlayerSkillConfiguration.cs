using CodventureV1.Domain.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodventureV1.Persistence.Configurations;

public sealed class PlayerSkillConfiguration : BaseConjunctionEntityConfiguration<Guid, int, PlayerSkill>
{
    public override void Configure(EntityTypeBuilder<PlayerSkill> builder)
    {
        builder
            .HasKey(x => new { x.PlayerId, x.SkillId });

        builder
            .Property(x => x.Proficiency)
            .HasDefaultValue(SkillConstants.DefaultPlayerProficiency);

        base.Configure(builder);
    }
}
