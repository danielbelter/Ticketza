using System;
using System.Threading.Tasks;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    [Route("events/{eventId}/tickets")]
    [Authorize]
    [ApiController]
    public class TicketsController : ApiControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Get(Guid eventId, Guid ticketId)
        {
            var ticket = _ticketService.GetTicket(UserId, eventId, ticketId);
            if (ticket == null)
            {
                return NotFound();
            }

            return Json(ticket);
        }

        [HttpGet("purchase/{amount}")]
        public async Task<IActionResult> Post(Guid eventId, int amount)
        {
            var ticket = _ticketService.PurchaseAsync(UserId, eventId, amount);
            return NoContent();
        }

        [HttpDelete("cancel/{amount}")]
        public async Task<IActionResult> Delete(Guid eventId, int amount)
        {
            await _ticketService.CancelAsync(UserId, eventId, amount);

            return NoContent();
        }
    }
}