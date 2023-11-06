using AutoMapper;
using CodventureV1.Application.Common.Commands.Create;
using CodventureV1.Domain.Skills;
using CodventureV1.Infrastructure.UnitOfWorks;

namespace CodventureV1.Application.Skills.Commands.CreateSkillType;

public sealed record CreateSkillTypeCommand(string Name, string Code) : CreateCommand<int>;
public sealed class Handler : CreateCommandHandler<CreateSkillTypeCommand, SkillType, int>
{
    public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
