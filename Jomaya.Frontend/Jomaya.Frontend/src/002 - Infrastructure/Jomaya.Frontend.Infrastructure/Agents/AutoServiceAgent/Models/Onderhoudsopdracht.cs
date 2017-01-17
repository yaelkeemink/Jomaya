// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Jomaya.Frontend.Infrastructure.Agents.AutoService.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Onderhoudsopdracht
    {
        /// <summary>
        /// Initializes a new instance of the Onderhoudsopdracht class.
        /// </summary>
        public Onderhoudsopdracht() { }

        /// <summary>
        /// Initializes a new instance of the Onderhoudsopdracht class.
        /// </summary>
        public Onderhoudsopdracht(int kilometerstand, long? id = default(long?), long? autoId = default(long?), Auto auto = default(Auto), bool? isApk = default(bool?), string werkzaamheden = default(string), int? status = default(int?), DateTime? steekproefDatum = default(DateTime?), DateTime? datum = default(DateTime?))
        {
            Id = id;
            AutoId = autoId;
            Auto = auto;
            Kilometerstand = kilometerstand;
            IsApk = isApk;
            Werkzaamheden = werkzaamheden;
            Status = status;
            SteekproefDatum = steekproefDatum;
            Datum = datum;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "autoId")]
        public long? AutoId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "auto")]
        public Auto Auto { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "kilometerstand")]
        public int Kilometerstand { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isApk")]
        public bool? IsApk { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "werkzaamheden")]
        public string Werkzaamheden { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int? Status { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "steekproefDatum")]
        public DateTime? SteekproefDatum { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "datum")]
        public DateTime? Datum { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (this.Auto != null)
            {
                this.Auto.Validate();
            }
        }
    }
}