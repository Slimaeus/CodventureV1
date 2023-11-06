namespace CodventureV1.Application.Common.Models;

public sealed record PaginationParams(int PageIndex = 1, int PageSize = 10, string SearchString = "") : IPaginationParams;
