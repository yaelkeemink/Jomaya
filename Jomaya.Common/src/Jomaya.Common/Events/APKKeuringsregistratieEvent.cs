using Minor.WSA.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Common.Events
{
    public class APKKeuringsregistratieEvent : DomainEvent
    {
        public string Kenteken { get; set; }        

        public DateTime SteekproefDatum { get; set; }
        public DateTime OnderhoudsDatum { get; set; }

        public long AutoId { get; set; }

        public long OnderhoudId { get; set; }

        public int Kilometerstand { get; set; }
        public OnderhoudStatus Status { get; set; }
        public string Werkzaamheden { get; set; }
    }
}
