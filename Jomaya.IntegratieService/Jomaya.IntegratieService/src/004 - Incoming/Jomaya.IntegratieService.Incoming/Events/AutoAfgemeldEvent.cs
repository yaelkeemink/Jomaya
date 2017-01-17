using Jomaya.IntegratieService.Entities;
using Minor.WSA.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.IntegratieService.Incoming.Events
{
    public class AutoAfgemeldEvent : DomainEvent
    {
        public Auto Auto { get; set; }
        public Eigenaar Eigenaar { get; set; }
    }
}
