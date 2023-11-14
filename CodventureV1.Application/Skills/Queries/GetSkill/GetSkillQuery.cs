using AutoMapper;
using CodventureV1.Application.Common.Queries.GetById;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Skills;
using CodventureV1.Persistence;

namespace CodventureV1.Application.Skills.Queries.GetSkill;

public sealed record GetSkillQuery(int Id) : GetByIdQuery<int, SkillDto>(Id);
public sealed class Handler
    (IQueryRepositoryFactory queryRepositoryFactory, ApplicationDbContext applicationDbContext, IMapper mapper)
    : GetByIdQueryHandler<int, GetSkillQuery, Skill, SkillDto>(queryRepositoryFactory, applicationDbContext, mapper)
{
}
