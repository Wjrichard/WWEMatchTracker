using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Predictions.Queries;

public class GetMatchPredictions
{
    public record Query(int MatchId) : IRequest<List<Prediction>>;

    public class Handler : IRequestHandler<Query, List<Prediction>>
    {
        private readonly PredictionRepository _predictionRepository;

        public Handler(PredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<List<Prediction>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.GetMatchPredictions(request.MatchId);
        }
    }
}
