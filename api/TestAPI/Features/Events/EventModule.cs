using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Features.Events.Commands;
using TestAPI.Features.Events.Queries;
using TestAPI.Models;

namespace TestAPI.Modules;

public static class EventModule
{
    public static void MapEventRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Events").WithTags("Events");

        group.MapGet("/GetEventDetails",
                async (IMediator mediator) => await mediator.Send(new GetEventDetails.Query()))
            .WithName(nameof(GetEventDetails))
            .Produces<List<EventDetails>>();

        group.MapPost("/AddEvent",
                async ([FromBody] Event e, IMediator mediator) => await mediator.Send(new AddEvent.Command(e)))
            .WithName(nameof(AddEvent))
            .Produces<int>();
    }
}