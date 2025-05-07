using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Features.Matches.Commands;
using TestAPI.Features.Users.Commands;
using TestAPI.Features.Users.Queries;
using TestAPI.Models;

namespace TestAPI.Features.Match;

public static class MatchModule
{
    public static void MapMatchRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Match").WithTags("Match");

        group.MapGet("/GetMatches",
                async (IMediator mediator) => await mediator.Send(new GetMatches.Query()))
            .WithName(nameof(GetMatches))
            .Produces<List<Models.Match>>();

        group.MapPost("/AddMatch",
                async ([FromBody] Models.Match match, IMediator mediator) => await mediator.Send(new AddMatch.Command(match)))
            .WithName(nameof(AddMatch))
            .Produces<int>();
    }
}