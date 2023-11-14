using AutoMapper;
using CodventureV1.Application.Common.Commands.Create;
using CodventureV1.Domain.Skills;
using CodventureV1.Infrastructure.UnitOfWorks;

namespace CodventureV1.Application.Skills.Commands.CreateSkill;

public sealed record CreateSkillCommand(string Name, string Code, int SkillTypeId) : CreateCommand<int>;
public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper)
    : CreateCommandHandler<CreateSkillCommand, Skill, int>(unitOfWork, mapper)
{
}
