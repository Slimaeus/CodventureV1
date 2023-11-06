using AutoMapper;
using CodventureV1.Application.Skills.Commands.CreateSkill;
using CodventureV1.Application.Skills.Commands.CreateSkillType;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Domain.Skills;

namespace CodventureV1.Application.Skills;

public sealed class SkillMappingProfile : Profile
{
    public SkillMappingProfile()
    {
        CreateMap<CreateSkillCommand, Skill>();
        CreateMap<CreateSkillTypeCommand, SkillType>();

        CreateMap<Skill, SkillDto>();
        CreateMap<SkillType, SkillTypeDto>();
    }
}
