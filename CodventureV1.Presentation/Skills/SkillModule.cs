using CodventureV1.Application.Skills.Commands.CreateSkill;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Application.Skills.Queries.GetSkill;
using CodventureV1.Application.Skills.Queries.GetSkills;
using CodventureV1.Domain.Skills;
using CodventureV1.Presentation.Common.Modules;

namespace CodventureV1.Presentation.Skills;

public sealed class SkillModule : EntityModule<int, Skill, SkillDto>
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        base.AddRoutes(app);

        MapGetWithPagination<GetSkillsQuery>();
        MapGetById<GetSkillQuery>();
        MapPostWithEntity<CreateSkillCommand, GetSkillQuery>();
    }
}
