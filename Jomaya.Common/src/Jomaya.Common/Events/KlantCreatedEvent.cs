using Minor.WSA.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Common.Events
{
    public class KlantCreatedEvent
        : DomainEvent
    {
        public long KlantId { get; set; }
        public string Voorletters { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Telefoonnummer { get; set; }
    }
}
