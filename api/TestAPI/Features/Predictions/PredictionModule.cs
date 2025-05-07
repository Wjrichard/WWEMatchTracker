using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Features.Matches.Commands;
using TestAPI.Features.Predictions.Queries;
using TestAPI.Features.Predictions.Commands;
using TestAPI.Models;

namespace TestAPI.Modules;

public static class PredictionModule
{
    public static void MapPredictionRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Prediction").WithTags("Prediction");

        group.MapGet("/GetAllPredictions",
                async (IMediator mediator) => await mediator.Send(new GetAllPredictions.Query()))
            .WithName(nameof(GetAllPredictions))
            .Produces<List<Prediction>>();

        group.MapGet("/GetMatchPredictions/{matchId}",
                async (IMediator mediator,int matchId) => await mediator.Send(new GetMatchPredictions.Query(matchId)))
            .WithName(nameof(GetMatchPredictions))
            .Produces<List<Prediction>>();

        group.MapPost("/AddPrediction",
                async ([FromBody] Prediction prediction, IMediator mediator) => await mediator.Send(new AddPrediction.Command(prediction)))
            .WithName(nameof(AddPrediction))
            .Produces<int>();
    }
}