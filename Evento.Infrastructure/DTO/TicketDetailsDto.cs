using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Infrastructure.DTO
{
    public class TicketDetailsDto : TicketDto
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
    }
}