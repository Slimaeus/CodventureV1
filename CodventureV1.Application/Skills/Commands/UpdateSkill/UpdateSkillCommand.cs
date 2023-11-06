using AutoMapper;
using CodventureV1.Application.Common.Commands.Update;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Skills;
using CodventureV1.Infrastructure.UnitOfWorks;

namespace CodventureV1.Application.Skills.Commands.UpdateSkill;

public sealed record UpdateSkillCommand(int Id, string Name, string Code) : UpdateCommand<int>(Id);
public sealed class Handler : UpdateCommandHandler<int, UpdateSkillCommand, Skill>
{
    public Handler(IUnitOfWork unitOfWork, IQueryRepositoryFactory queryRepositoryFactory, IMapper mapper) : base(unitOfWork, queryRepositoryFactory, mapper)
    {
    }
}
