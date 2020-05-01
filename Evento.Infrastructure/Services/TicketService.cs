using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using System.Linq;
using Evento.Core.Domain;
using Evento.Infrastructure.Extensions;

namespace Evento.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private IUserRepository _userRepository;
        private IEventRepository _eventRepository;
        private IMapper _mapper;

        public TicketService(IUserRepository userRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDetailsDto>> GetForUserAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var events = await _eventRepository.BrowseBy();
            var allTickets = new List<TicketDetailsDto>();
            foreach (var eventEntity in events)
            {
                var tickets = _mapper.Map<IEnumerable<TicketDetailsDto>>(eventEntity.GetTicketsPurchasedByUser(user))
                    .ToList();
                tickets.ForEach(x =>
                {
                    x.EventId = eventEntity.Id;
                    x.Name = eventEntity.Name;
                });
                allTickets.AddRange(tickets);
            }

            return allTickets;
        }

        public async Task<TicketDto> GetTicket(Guid userId, Guid eventId, Guid ticketId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var ticket = await _eventRepository.GetTicketOrFailAsync(eventId, ticketId);

            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task PurchaseAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var eventEntity = await _eventRepository.GetOrFailAsync(eventId);
            eventEntity.PurchaseTickets(user, amount);
            await _eventRepository.UpdateEvent(eventEntity);
        }

        public async Task CancelAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var eventEntity = await _eventRepository.GetOrFailAsync(eventId);
            eventEntity.CancelPurchasedTickets(user, amount);
            await _eventRepository.UpdateEvent(eventEntity);
        }
    }
}