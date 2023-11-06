using AutoMapper;
using CodventureV1.Application.Common.Models;
using CodventureV1.Application.Common.Queries.GetWithPagination;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Skills;

namespace CodventureV1.Application.Skills.Queries.GetSkills;

public sealed record GetSkillsQuery(IPaginationParams Params) : GetWithPaginationQuery<SkillDto>(Params);
public sealed class Handler : GetWithPaginationHandler<int, Skill, GetSkillsQuery, SkillDto>
{
    public Handler(IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper) : base(queryRepositoryFactory, mapper)
    {
    }
}
