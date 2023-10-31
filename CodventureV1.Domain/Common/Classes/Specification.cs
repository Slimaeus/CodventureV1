using CodventureV1.Domain.Common.Interfaces;

namespace CodventureV1.Domain.Common.Classes;

public record Specification(int PageIndex = 1, int PageSize = 10) : ISpecification;
