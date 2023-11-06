using Carter;
using CodventureV1.Application.Common.Models;
using CodventureV1.Application.Skills.Commands.CreateSkill;
using CodventureV1.Application.Skills.Dtos;
using CodventureV1.Application.Skills.Queries.GetSkill;
using CodventureV1.Application.Skills.Queries.GetSkills;
using CodventureV1.Domain.Results.Interfaces;
using CodventureV1.Domain.Skills;
using CodventureV1.Infrastructure.Repositories.Queries.Collections;
using CodventureV1.Presentation.Common.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodventureV1.Presentation.Skills;

public sealed class SkillModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Skills")
            .WithTags(nameof(Skill));

        group.MapGet("", Get);
        group.MapGet("{id:int}", GetById);
        group.MapPost("", Post);
    }

    private static async Task<Results<
        Ok<IResult<IPagedList<SkillDto>>>,
        BadRequest<ProblemDetails>>>
        Get(ISender sender, [AsParameters] PaginationParams @params)
        => ResultHandlers.HandleGetResult(await sender.Send(new GetSkillsQuery(@params)));

    private static async Task<Results<
        Ok<IResult<SkillDto>>,
        NotFound<ProblemDetails>>>
        GetById(ISender sender, [FromRoute] int id)
        => ResultHandlers.HandleGetByIdResult(await sender.Send(new GetSkillQuery(id)));

    private static async Task<Results<
        Ok<IResult<int>>,
        BadRequest<ProblemDetails>>>
        Post(ISender sender, [FromBody] CreateSkillCommand request)
        => ResultHandlers.HandlePostResult(await sender.Send(request));
}
