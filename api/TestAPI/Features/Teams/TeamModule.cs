using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Features.Teams.Commands;
using TestAPI.Features.Teams.Queries;
using TestAPI.Models;

namespace TestAPI.Features.Teams;

public static class TeamModule
{
    public static void MapTeamRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Teams").WithTags("Teams");

        group.MapGet("/GetTeams",
                async (IMediator mediator) => await mediator.Send(new GetAllTeams.Query()))
            .WithName(nameof(GetAllTeams))
            .Produces<List<Team>>();

        group.MapPost("/SaveTeam",
                async ([FromBody] Team team, IMediator mediator) => await mediator.Send(new SaveTeam.Command(team)))
            .WithName("SaveTeam")
            .Produces<int>();
    }
}