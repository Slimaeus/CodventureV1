using AutoMapper;
using CodventureV1.Application.Common.Queries;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Domain.Skills;
using CodventureV1.Persistence;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Skills.Queries.GetSkill;

public sealed record GetSkillQuery(int Id) : IQuery<SkillDto>;
public sealed class Handler : IQueryHandler<GetSkillQuery, SkillDto>
{
    private readonly IQueryRepository<int, Skill> _repository;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public Handler(IQueryRepositoryFactory queryRepositoryFactory, ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _repository = queryRepositoryFactory.CreateQueryRepository<int, Skill>();
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<IResult<SkillDto>> Handle(GetSkillQuery request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id);

        if (player is null)
        {
            var error = SkillErrors.SkillNotFound;
            return Result.Fail<SkillDto>(error, StatusCodes.Status404NotFound);
        }
        _applicationDbContext.Entry(player).Reference(x => x.Type).Load();
        var playerDto = _mapper.Map<SkillDto>(player);
        return Result.Ok(playerDto);
    }
}
