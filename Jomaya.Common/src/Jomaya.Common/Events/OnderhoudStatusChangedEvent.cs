using Minor.WSA.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Common.Events
{
    public class OnderhoudStatusChangedEvent : DomainEvent
    {
        public long OnderhoudsopdrachtId { get; set; }
        public long AutoId { get; set; }     
        public int Kilometerstand { get; set; }
        public bool IsApk { get; set; }
        public string Werkzaamheden { get; set; }
        public int Status { get; set; }
        public DateTime SteekproefDatum { get; set; }
        public DateTime OnderhoudsDatum { get; set; }
    }
}

