namespace CodventureV1.Domain.Common.Interfaces;

public interface IConjunctionEntity<TKey1, TKey2>
{
    TKey1 Id1 { get; }
    TKey2 Id2 { get; }
}
