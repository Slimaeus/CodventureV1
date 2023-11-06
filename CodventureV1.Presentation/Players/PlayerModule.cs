using Carter;
using CodventureV1.Application.Common.Models;
using CodventureV1.Application.Players.Dtos;
using CodventureV1.Application.Players.Queries.GetPlayer;
using CodventureV1.Application.Players.Queries.GetPlayers;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Infrastructure.Repositories.Collections;
using CodventureV1.Presentation.Common.Handlers;
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

    private static async Task<Results<
        Ok<IResult<IPagedList<PlayerDto>>>,
        BadRequest<ProblemDetails>>>
        Get(ISender sender, [AsParameters] PaginationParams @params)
        => ResultHandlers.HandleGetResult(await sender.Send(new GetPlayersQuery(@params)));

    private static async Task<Results<
        Ok<IResult<PlayerDto>>,
        NotFound<ProblemDetails>>>
        GetById(ISender sender, [FromRoute] Guid id)
        => ResultHandlers.HandleGetByIdResult(await sender.Send(new GetPlayerQuery(id)));
}
