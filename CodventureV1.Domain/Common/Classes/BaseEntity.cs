using CodventureV1.Domain.Common.Interfaces;

namespace CodventureV1.Domain.Common.Classes;

public abstract class BaseEntity<TKey> : IEntity<TKey>
{
    public abstract TKey Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
}