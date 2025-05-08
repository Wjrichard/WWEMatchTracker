using MediatR;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Features.Events.Commands;

public class AddEvent
{
    public record Command(Event Event) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly EventRepository _eventRepository;

        public Handler(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) => await _eventRepository.AddEvent(request.Event);
    }
}


