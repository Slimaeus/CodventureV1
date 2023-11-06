namespace CodventureV1.Application.Common.Models;

public interface IPaginationParams
{
    int PageIndex { get; init; }
    int PageSize { get; init; }
    string SearchString { get; init; }
}