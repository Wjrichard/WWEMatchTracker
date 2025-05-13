using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Features.Matches.Commands;
using TestAPI.Features.Matches.Queries;
using TestAPI.Models;

namespace TestAPI.Features.Match;

public static class MatchModule
{
    public static void MapMatchRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Match").WithTags("Match");

        group.MapGet("/GetMatchDetailsForEvent/{eventId}",
                async (IMediator mediator,int eventId) => await mediator.Send(new GetMatchDetailsForEvent.Query(eventId)))
            .WithName(nameof(GetMatchDetailsForEvent))
            .Produces<List<EventDetails>>();

        group.MapGet("/GetMatchParticipants",
                async (IMediator mediator,int matchId) => await mediator.Send(new GetMatchParticipants.Query(matchId)))
            .WithName(nameof(GetMatchParticipants))
            .Produces<List<Team>>();

        group.MapPost("/AddMatch",
                async ([FromBody] Models.Match match, IMediator mediator) => await mediator.Send(new AddMatch.Command(match)))
            .WithName(nameof(AddMatch))
            .Produces<int>();
    }
}