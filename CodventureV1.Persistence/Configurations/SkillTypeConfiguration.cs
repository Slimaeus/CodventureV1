using CodventureV1.Domain.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodventureV1.Persistence.Configurations;

public sealed class SkillTypeConfiguration : IEntityTypeConfiguration<SkillType>
{
    public void Configure(EntityTypeBuilder<SkillType> builder)
    {
        builder
           .Property(x => x.Id)
           .ValueGeneratedOnAdd();

        builder
            .HasIndex(x => x.Code)
            .IsUnique();

        builder
            .Property(x => x.Name)
            .HasMaxLength(SkillConstants.NameMaxLength);
    }
}
