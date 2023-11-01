using AutoMapper;
using CodventureV1.Application.Common.Queries;
using CodventureV1.Application.Players.Dtos;
using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Players;
using CodventureV1.Domain.Results.Classes;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Persistence;
using Microsoft.AspNetCore.Http;

namespace CodventureV1.Application.Players.Queries.GetPlayer;

public sealed record GetPlayerQuery(Guid Id) : IQuery<PlayerDto>;

public sealed class Handler : IQueryHandler<GetPlayerQuery, PlayerDto>
{
    private readonly IQueryRepository<Guid, Player> _repository;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public Handler(IQueryRepositoryFactory queryRepositoryFactory, ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _repository = queryRepositoryFactory.CreateQueryRepository<Guid, Player>();
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<IResult<PlayerDto>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id);

        if (player is null)
        {
            var error = PlayerErrors.PlayerNotFound;
            return Result.Fail<PlayerDto>(error, StatusCodes.Status404NotFound);
        }
        _applicationDbContext.Entry(player).Collection(x => x.PlayerSkills).Load();
        var playerDto = _mapper.Map<PlayerDto>(player);
        return Result.Ok(playerDto);
    }
}
