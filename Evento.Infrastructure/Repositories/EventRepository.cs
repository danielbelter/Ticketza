using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private static readonly ISet<Event> _events = new HashSet<Event>()
        {
            new Event(Guid.NewGuid(), "Event 1", "sample desc.", DateTime.Now, DateTime.Now)
        };

        public async Task<Event> GetEventById(Guid id)
        {
            return await Task.FromResult(_events.SingleOrDefault(x => x.Id == id));
        }

        public async Task<Event> GetEventByName(string name)
        {
            return await Task.FromResult(_events.SingleOrDefault(x =>
                x.Name.ToLowerInvariant() == name.ToLowerInvariant()));
        }

        public async Task<IEnumerable> BrowseBy(string name = "")
        {
            var events = _events.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                events = events.Where(x => x.Name.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant())
                );
            }

            return await Task.FromResult(events);
        }

        public async Task AddEvent(Event eventEntity)
        {
            _events.Add(eventEntity);
            await Task.CompletedTask;
        }

        public async Task UpdateEvent(Event eventEntity)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteEvent(Event eventEntity)
        {
            _events.Remove(eventEntity);
            await Task.CompletedTask;
        }
    }
}