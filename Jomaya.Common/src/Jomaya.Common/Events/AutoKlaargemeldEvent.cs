using Minor.WSA.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Common.Events
{
    public class AutoKlaargemeldEvent : DomainEvent
    {
        public long AutoId { get; set; }
        public long OnderhoudId { get; set; }
        public long KlantId { get; set; }
        public string Kenteken { get; set; }
        public int KilometerStand { get; set; }
        public int VoertuigType { get; set; }
        public bool IsApk { get; set; }
        public string EigenaarNaam { get; set; }        
        public string Werkzaamheden { get; set; }
        public DateTime OnderhoudsDatum { get; set; }

    }
}
