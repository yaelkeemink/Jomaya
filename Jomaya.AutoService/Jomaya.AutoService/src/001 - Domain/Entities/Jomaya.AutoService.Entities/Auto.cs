using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jomaya.AutoService.Entities
{
    public class Auto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Kenteken { get; set; }

        [Required]
        public string Type { get; set; } = "personenauto";

        public long KlantId { get; set; }
    }
}