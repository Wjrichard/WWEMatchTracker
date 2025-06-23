using MediatR;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Features.Matches.Commands;

public class SaveMatch
{
    public record Command(TestAPI.Models.Match Match) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly MatchRepository _matchRepository;

        public Handler(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.Match.MatchId > 0)
            {
                return await _matchRepository.UpdateMatch(request.Match);
            }
            else
            {
                return await _matchRepository.AddMatch(request.Match);
            }
        }
    }
}