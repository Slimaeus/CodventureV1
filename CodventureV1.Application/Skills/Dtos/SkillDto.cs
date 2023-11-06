namespace CodventureV1.Application.Skills.Dtos;

public sealed record SkillDto(int Id, string Name, string Code, SkillTypeDto Type);
