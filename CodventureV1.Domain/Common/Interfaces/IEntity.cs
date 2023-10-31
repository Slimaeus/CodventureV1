namespace CodventureV1.Domain.Common.Interfaces;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}