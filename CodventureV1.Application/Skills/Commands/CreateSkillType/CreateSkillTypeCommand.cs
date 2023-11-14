using AutoMapper;
using CodventureV1.Application.Common.Commands.Create;
using CodventureV1.Domain.Skills;
using CodventureV1.Infrastructure.UnitOfWorks;

namespace CodventureV1.Application.Skills.Commands.CreateSkillType;

public sealed record CreateSkillTypeCommand(string Name, string Code) : CreateCommand<int>;
public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : CreateCommandHandler<CreateSkillTypeCommand, SkillType, int>(unitOfWork, mapper)
{
}
