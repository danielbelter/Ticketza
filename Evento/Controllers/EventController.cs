using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands.Event;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var events = await _eventService.BrowseAsync(name);

            return Json(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var eventEntity = await _eventService.GetAsync(eventId);
            if (eventEntity == null)
            {
                return NotFound();
            }
            return Json(eventEntity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEvent createEvent)
        {
            var eventId = Guid.NewGuid();
            await _eventService.CreateAsync(eventId, createEvent.Name, createEvent.Description,
                createEvent.StartDate, createEvent.EndDate);
            await _eventService.AddTicketsAsync(eventId, createEvent.Tickets, createEvent.Price);
            return Created($"/events/{eventId}", null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody] UpdateEvent updateEvent)
        {
            await _eventService.UpdateAsync(eventId, updateEvent.Name, updateEvent.Description);
            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            await _eventService.DeleteAsync(eventId);
            return NoContent();
        }
    }
}