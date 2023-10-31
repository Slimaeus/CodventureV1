using Carter;
using CodventureV1.Application.Players.Queries.GetPlayer;
using CodventureV1.Application.Players.Queries.GetPlayers;
using CodventureV1.Domain.Common.Classes;
using CodventureV1.Domain.Players;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Presentation.Players.Constants;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodventureV1.Presentation.Players;

public sealed class PlayerModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(PlayerRoutes.GroupPattern)
            .WithTags(PlayerRoutes.Tag);

        group.MapGet(PlayerRoutes.Get, Get);
        group.MapGet(PlayerRoutes.GetById, GetById);
    }

    private static async Task<
        Ok<IResult<IEnumerable<Player>>>>
        Get(ISender sender, [AsParameters] Specification specification)
    {
        var result = await sender.Send(new GetPlayersQuery(specification));
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => TypedResults.Ok(result),
            _ => throw new Exception()
        };
    }

    private static async Task<Results<
        Ok<IResult<Player>>,
        NotFound<IDictionary<string, List<string>>>>>
        GetById(ISender sender, [FromRoute] Guid id)
    {
        var result = await sender.Send(new GetPlayerQuery(id));
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => TypedResults.Ok(result),
            StatusCodes.Status404NotFound => TypedResults.NotFound(result.Errors),
            _ => throw new Exception(),
        };
    }
}
