using AutoMapper;
using CodventureV1.Application.Common.Commands;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Domain.Skills;
using CodventureV1.Infrastructure.Repositories.Commands;
using CodventureV1.Infrastructure.UnitOfWorks;

namespace CodventureV1.Application.Skills.Commands.CreateSkill;

public sealed record CreateSkillCommand(string Name, string Code, int SkillTypeId) : ICommand<int>;
public sealed class Handler : ICommandHandler<CreateSkillCommand, int>
{
    private readonly ICommandRepository<Skill> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Handler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = unitOfWork.Repository<Skill>();
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IResult<int>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = _mapper.Map<Skill>(request);

        var result = await _repository.AddAsync(skill, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Result.Ok(result.Id);
    }
}
