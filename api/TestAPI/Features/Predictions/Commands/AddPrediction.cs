using MediatR;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Features.Predictions.Commands;

public class AddPrediction
{
    public record Command(Prediction Prediction) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly PredictionRepository _predictionRepository;

        public Handler(PredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) => await _predictionRepository.AddPrediction(request.Prediction);
    }
}


