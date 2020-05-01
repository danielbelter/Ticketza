using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static async Task<Event> GetOrFailAsync(this IEventRepository repository, Guid id)
        {
            var eventEntity = await repository.GetEventById(id);
            if (eventEntity == null)
            {
                throw new Exception($"Event with id: '{id}' does not exist.");
            }

            return eventEntity;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetUserById(id);
            if (user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return user;
        }

        public static async Task<Event> GetTicketOrFailAsync(this IEventRepository repository, Guid eventId, Guid ticketId)
        {
            var eventEntity = await repository.GetOrFailAsync(eventId);
            var ticket = eventEntity.Tickets.SingleOrDefault(x => x.Id == ticketId);

            if (ticket == null)
            {
                throw new Exception($"Ticket with id: '{ticket}' was not found for {eventEntity.Name}");
            }

            return eventEntity;
        }

    }
}