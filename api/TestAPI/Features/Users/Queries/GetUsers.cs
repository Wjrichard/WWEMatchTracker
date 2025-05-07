using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Users.Queries;

public class GetUsers
{
    public record Query : IRequest<List<User>>;
    public class Handler : IRequestHandler<Query, List<User>>
    {
        private readonly UserRepository _userRepository;

        public Handler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUsers();
        }
    }
}
