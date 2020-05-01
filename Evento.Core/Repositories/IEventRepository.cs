using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Evento.Core.Domain;

namespace Evento.Core.Repositories
{
    public interface IEventRepository
    {
        Task<Event> GetEventById(Guid id);
        Task<Event> GetEventByName(string name);
        Task<IEnumerable<Event>> BrowseBy(string name = "");
        Task AddEvent(Event eventEntity);
        Task UpdateEvent(Event eventEntity);
        Task DeleteEvent(Event eventEntity);
    }
}