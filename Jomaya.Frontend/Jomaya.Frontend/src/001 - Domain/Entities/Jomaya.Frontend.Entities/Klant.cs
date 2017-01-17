using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Entities
{
    public class Klant
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Voorletters zijn verplicht.")]
        public string Voorletters { get; set; }
        public string Tussenvoegsel { get; set; }
        [Required(ErrorMessage ="Achternaam is verplicht.")]
        public string Achternaam { get; set; }
        public string Telefoonnummer { get; set; }
    }
}
