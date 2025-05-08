using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Events.Queries;

public class GetEvents
{
    public record Query : IRequest<List<Event>>;
    public class Handler : IRequestHandler<Query, List<Event>>
    {
        private readonly EventRepository _eventRepository;

        public Handler(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetEvents();
        }
    }
}