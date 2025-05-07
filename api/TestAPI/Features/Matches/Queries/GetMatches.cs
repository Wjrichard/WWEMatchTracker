using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Users.Queries;

public class GetMatches
{
    public record Query : IRequest<List<Models.Match>>;
    public class Handler : IRequestHandler<Query, List<Models.Match>>
    {
        private readonly MatchRepository _matchRepository;

        public Handler(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<List<Models.Match>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _matchRepository.GetMatches();
        }
    }
}
