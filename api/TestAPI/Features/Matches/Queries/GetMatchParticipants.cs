using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Matches.Queries;

public class GetMatchParticipants
{
    public record Query(int matchId) : IRequest<List<Team>>;
    public class Handler : IRequestHandler<Query, List<Team>>
    {
        private readonly MatchRepository _matchRepository;

        public Handler(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<List<Team>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _matchRepository.GetMatchParticipants(request.matchId);
        }
    }
}
