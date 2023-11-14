using AutoMapper;
using CodventureV1.Application.Common.Models;
using CodventureV1.Application.Common.Queries.GetWithPagination;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Skills;

namespace CodventureV1.Application.Skills.Queries.GetSkillTypes;

public sealed record GetSkillTypesQuery(IPaginationParams Params) : GetWithPaginationQuery<SkillTypeDto>(Params);
public sealed class Handler(IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper) : GetWithPaginationHandler<int, SkillType, GetSkillTypesQuery, SkillTypeDto>(queryRepositoryFactory, mapper)
{
}
