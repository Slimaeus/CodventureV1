using AutoMapper;
using CodventureV1.Application.Players.Dtos;
using CodventureV1.Domain.Players;

namespace CodventureV1.Application.Players;

public sealed class PlayerMappingProfile : Profile
{
    public PlayerMappingProfile()
    {
        CreateMap<Player, PlayerDto>();
    }
}
