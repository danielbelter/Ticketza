using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;

namespace Evento.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDetailsDto> GetAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetEventById(id);

            return _mapper.Map<EventDetailsDto>(eventEntity);
        }

        public async Task<EventDetailsDto> GetAsync(string name)
        {
            var eventEntity = await _eventRepository.GetEventByName(name);

            return _mapper.Map<EventDetailsDto>(eventEntity);
        }

        public async Task<IEnumerable<EventDto>> BrowseAsync(string name = null)
        {
            var events = await _eventRepository.BrowseBy(name);
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var eventEntity = await _eventRepository.GetEventByName(name);
            if (eventEntity != null)
                throw new Exception($"Event named: {name} already exist.");
            eventEntity = new Event(id, name, description, startDate, endDate);
            await _eventRepository.AddEvent(eventEntity);
        }

        public async Task AddTicketsAsync(Guid eventId, int amount, decimal price)
        {
            var eventEntity = await _eventRepository.GetOrFailAsync(eventId);

            eventEntity.AddTickets(amount, price);
            await _eventRepository.UpdateEvent(eventEntity);
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var eventEntity = await _eventRepository.GetEventByName(name);
            if (eventEntity != null)
            {
                throw new Exception($"Event named: '{name}' already exists.");
            }

            eventEntity = await _eventRepository.GetOrFailAsync(id);
            eventEntity.SetName(name);
            eventEntity.SetDescription(description);
            await _eventRepository.UpdateEvent(eventEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetOrFailAsync(id);
            await _eventRepository.DeleteEvent(eventEntity);
        }
    }
}