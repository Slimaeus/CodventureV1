using AutoMapper;
using CodventureV1.Application.Common.Models;
using CodventureV1.Domain.Common.Classes;

namespace CodventureV1.Application.Common;

public sealed class CommonMappingProfile : Profile
{
    public CommonMappingProfile()
    {
        CreateMap(typeof(PaginationParams), typeof(Specification<>));
    }
}
