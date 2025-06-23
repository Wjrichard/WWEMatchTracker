using MediatR;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Features.Teams.Commands;

public class SaveTeam
{
    public record Command(Team Team) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly TeamRepository _teamRepository;

        public Handler(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.Team.TeamId > 0)
            {
                return await _teamRepository.UpdateTeam(request.Team);
            }
            else
            {
                return await _teamRepository.AddTeam(request.Team);
            }
        }
    }
}