using MediatR;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Features.Matches.Commands;

public class AddMatch
{
    public record Command(TestAPI.Models.Match Match) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly MatchRepository _matchRepository;

        public Handler(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) => await _matchRepository.AddMatch(request.Match);
    }
}
