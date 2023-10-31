using CodventureV1.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodventureV1.Persistence.Configurations;

public sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder
            .Property(x => x.TypingSpeed)
            .HasDefaultValue(PlayerConstants.DefaultTypingSpeed);

        builder
            .Property(x => x.LearningSpeed)
            .HasDefaultValue(PlayerConstants.DefaultLearningSpeed);
    }
}
