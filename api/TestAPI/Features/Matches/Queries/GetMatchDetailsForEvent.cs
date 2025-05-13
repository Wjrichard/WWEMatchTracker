using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Matches.Queries;

public class GetMatchDetailsForEvent
{
    public record Query(int EventId) : IRequest<List<MatchDetails>>;
    public class Handler : IRequestHandler<Query, List<MatchDetails>>
    {
        private readonly MatchRepository _matchRepository;

        public Handler(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<List<MatchDetails>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _matchRepository.GetMatchDetails(request.EventId);
        }
    }
}
