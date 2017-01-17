using Jomaya.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Entities
{
    public class Onderhoudsopdracht
    {
        public long Id { get; set; }
        public long AutoId { get; set; }

        public Auto Auto { get; set; }

        public OnderhoudStatus Status { get; set; }
        
        public int Kilometerstand { get; set; }

        public bool IsApk { get; set; } = false;
        public string Werkzaamheden { get; set; }
        public DateTime Datum { get; set; }
        public DateTime SteekproefDatum { get; set; }
    }
}
