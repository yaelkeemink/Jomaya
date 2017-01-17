using Minor.WSA.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Common.Events
{
    public class AutoCreatedEvent : DomainEvent
    {
        public long AutoId { get; set; }
        
        public string Kenteken { get; set; }
        
        public string Type { get; set; }

        public long KlantId { get; set; }
    }
}
