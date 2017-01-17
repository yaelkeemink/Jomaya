using Jomaya.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.AutoService.Entities
{
    public class Onderhoudsopdracht
    {
        [Key]
        public long Id { get; set; }
        public long AutoId { get; set; }

        public Auto Auto { get; set; }

        [Required]
        public int Kilometerstand { get; set; }

        public bool IsApk { get; set; }

        public string Werkzaamheden { get; set; }

        public OnderhoudStatus Status { get; set; }
        public DateTime SteekproefDatum { get; set; }
        public DateTime Datum { get; set; }
    }
}