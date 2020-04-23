using System;
using System.Collections.Generic;
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
    }
}