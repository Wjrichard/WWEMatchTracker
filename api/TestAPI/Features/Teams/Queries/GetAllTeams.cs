using MediatR;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Features.Teams.Queries
{
    public class GetAllTeams
    {
        public record Query() : IRequest<List<Team>>;

        public class Handler : IRequestHandler<Query, List<Team>>
        {
            private readonly TeamRepository _teamRepository;

            public Handler(TeamRepository teamRepository)
            {
                _teamRepository = teamRepository;
            }

            public async Task<List<Team>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _teamRepository.GetTeams();
            }
        }
    }
}