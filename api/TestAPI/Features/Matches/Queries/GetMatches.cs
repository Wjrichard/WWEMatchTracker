using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Matches.Queries;

public class GetMatches
{
    public record Query(int EventId) : IRequest<EventDetails>;
    public class Handler : IRequestHandler<Query, EventDetails>
    {
        private readonly MatchRepository _matchRepository;

        public Handler(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<EventDetails> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _matchRepository.GetMatches(request.EventId);
        }
    }
}
