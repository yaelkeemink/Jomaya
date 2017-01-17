using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Entities
{
    public class Auto
    {
        [Required(ErrorMessage = "Klant is niet meegegeven, ga terug naar de vorige pagina.")]
        public long KlantId { get; set; }
        public Klant klant { get; set; }
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Kenteken is verplicht.")]
        public string Kenteken { get; set; }

        public string Type { get; set; } = "personenauto";
    }
}
