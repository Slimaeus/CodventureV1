using CodventureV1.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodventureV1.Persistence.Configurations;

public abstract class BaseConjunctionEntityConfiguration<TKey1, TKey2, TConjunctionEntity> : IEntityTypeConfiguration<TConjunctionEntity>
    where TConjunctionEntity : class, IConjunctionEntity<TKey1, TKey2>
{
    public virtual void Configure(EntityTypeBuilder<TConjunctionEntity> builder)
        => builder
            .Ignore(x => x.Id1)
            .Ignore(x => x.Id2);
}
