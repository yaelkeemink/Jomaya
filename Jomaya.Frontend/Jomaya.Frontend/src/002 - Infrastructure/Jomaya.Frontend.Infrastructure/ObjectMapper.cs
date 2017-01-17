using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jomaya.Frontend.Entities;
using Jomaya.Common;

namespace Jomaya.Frontend.Infrastructure
{
    public class ObjectMapper
    {
        public static Entities.Klant ConvertKlant(Agents.KlantService.Models.Klant klant)
        {
            var newKlant = new Entities.Klant()
            {
                Id = klant.Id.HasValue ? klant.Id.Value : 0,
                Voorletters = klant.Voorletters,
                Tussenvoegsel = klant.Tussenvoegsel,               
                Achternaam = klant.Achternaam,
                Telefoonnummer = klant.Telefoonnummer,               
            };              

            return newKlant; 
        }

        public static Agents.KlantService.Models.Klant ConvertKlant(Entities.Klant klant)
        {
            var newKlant = new Agents.KlantService.Models.Klant()
            {
                Id = klant.Id == 0 ? new long?() : klant.Id,
                Achternaam= klant.Achternaam,
                Voorletters = klant.Voorletters,
                Tussenvoegsel = klant.Tussenvoegsel,
                Telefoonnummer = klant.Telefoonnummer,
            };

            return newKlant;
        }


        public static Entities.Auto ConvertAuto(Agents.AutoService.Models.Auto auto)
        {
            var newAuto = new Entities.Auto()
            {
                Id = auto.Id.HasValue ? auto.Id.Value : 0,
                Kenteken = auto.Kenteken,
                KlantId = auto.KlantId.HasValue ? auto.KlantId.Value : 0,
                Type = auto.Type

            };

            return newAuto;
        }

        public static Agents.AutoService.Models.Auto ConvertAuto(Entities.Auto auto)
        {
            var newAuto = new Agents.AutoService.Models.Auto()
            {
                Id = auto.Id == 0 ? new long?() : auto.Id,
                Kenteken = auto.Kenteken,
                KlantId = auto.KlantId == 0 ? new long?() : auto.KlantId,
                Type = auto.Type

            };

            return newAuto;
        }

        public static Entities.Onderhoudsopdracht ConvertOnderhoudsOpdracht(Agents.AutoService.Models.Onderhoudsopdracht onderhoudsopdracht)
        {
            var newOnderhoudsOpdracht = new Entities.Onderhoudsopdracht()
            {
                Id = onderhoudsopdracht.Id.HasValue ? onderhoudsopdracht.Id.Value : 0,
                IsApk = onderhoudsopdracht.IsApk.HasValue ? onderhoudsopdracht.IsApk.Value : false,
                Kilometerstand = onderhoudsopdracht.Kilometerstand,
                AutoId = onderhoudsopdracht.AutoId.HasValue ? onderhoudsopdracht.AutoId.Value : 0,
                Status = onderhoudsopdracht.Status.HasValue ? (OnderhoudStatus)onderhoudsopdracht.Status : OnderhoudStatus.Aangemeld,
                Werkzaamheden = onderhoudsopdracht.Werkzaamheden,
                Datum = onderhoudsopdracht.Datum ?? DateTime.Now,
                SteekproefDatum = onderhoudsopdracht.SteekproefDatum ?? DateTime.Now,
            };
            return newOnderhoudsOpdracht;
        }

        public static Agents.AutoService.Models.Onderhoudsopdracht ConvertOnderhoudsOpdracht(Entities.Onderhoudsopdracht onderhoudsopdracht)
        {
            var newOnderhoudsOpdracht = new Agents.AutoService.Models.Onderhoudsopdracht()
            {
                Id = onderhoudsopdracht.Id,
                IsApk = onderhoudsopdracht.IsApk,
                Kilometerstand = onderhoudsopdracht.Kilometerstand,
                AutoId = onderhoudsopdracht.AutoId,
                Status = (int)onderhoudsopdracht.Status,
                Werkzaamheden = onderhoudsopdracht.Werkzaamheden,
                Datum = onderhoudsopdracht.Datum,
                SteekproefDatum = onderhoudsopdracht.SteekproefDatum,
            };
            return newOnderhoudsOpdracht;
        }
    }
}
