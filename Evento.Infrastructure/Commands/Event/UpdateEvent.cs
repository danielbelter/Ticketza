using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Infrastructure.Commands.Event
{
    public class UpdateEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}