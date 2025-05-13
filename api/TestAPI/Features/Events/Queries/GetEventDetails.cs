using MediatR;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPI.Features.Events.Queries;

public class GetEventDetails
{
    public record Query : IRequest<List<EventDetails>>;
    public class Handler : IRequestHandler<Query, List<EventDetails>>
    {
        private readonly EventRepository _eventRepository;

        public Handler(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventDetails>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetEventDetails();
        }
    }
}