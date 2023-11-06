using CodventureV1.Application.Skills.Commands.CreateSkillType;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Domain.Skills;
using CodventureV1.Presentation.Common.Modules;

namespace CodventureV1.Presentation.Skills;

public sealed class SkillTypeModule : EntityModule<int, SkillType, SkillTypeDto>
{
    protected override void MapEndpoints()
    {
        MapPost<CreateSkillTypeCommand>();
    }
}
