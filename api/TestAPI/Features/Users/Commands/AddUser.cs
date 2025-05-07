using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Users.Commands;

public class AddUser
{
    public record Command(User User) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly UserRepository _userRepository;

        public Handler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) => await _userRepository.AddUser(request.User);
    }
}
