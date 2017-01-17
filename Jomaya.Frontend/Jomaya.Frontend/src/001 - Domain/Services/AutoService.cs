using System;
using Minor.WSA.Commons;
using Jomaya.AutoService.Entities;
using Jomaya.Common.Events;
using Jomaya.AutoService.Services.Interfaces;

namespace Jomaya.AutoService.Services
{
    public class AutoService : IDisposable
    {
        private readonly IRepository<Auto, long> _autoRepository;
        private readonly IRepository<Onderhoudsopdracht, long> _onderhoudRepository;
        private readonly IEventPublisher _publisher;

        public AutoService(IRepository<Auto, long> autoRepository, IRepository<Onderhoudsopdracht, long> onderhoudRepository, IEventPublisher publisher)
        {
            _autoRepository = autoRepository;
            _onderhoudRepository = onderhoudRepository;
            _publisher = publisher;
        }

        public Auto CreateAuto(Auto auto)
        {
            _autoRepository.Insert(auto);

            AutoCreatedEvent autoCreatedEvent = CreateAutoCreatedEvent(auto);
            _publisher.Publish(autoCreatedEvent);

            return auto;
        }

        public Onderhoudsopdracht CreateOnderhoudsopdracht(Onderhoudsopdracht onderhoudsopdracht)
        {
            _onderhoudRepository.Insert(onderhoudsopdracht);

            OnderhoudsopdrachtCreatedEvent onderhoudsOpdrachtCreatedEvent = CreateOnderhoudsopdrachtCreatedEvent(onderhoudsopdracht);
            _publisher.Publish(onderhoudsOpdrachtCreatedEvent);

            return onderhoudsopdracht;
        }


        public Onderhoudsopdracht UpdateOnderhoudsopdracht(Onderhoudsopdracht onderhoudsopdracht)
        {
            Onderhoudsopdracht oldOnderhoudsopdracht = _onderhoudRepository.Find(onderhoudsopdracht.Id);
            
            bool statusChanged = false;
            bool klaargemeldEvent = false;
            
            if (oldOnderhoudsopdracht.Status != onderhoudsopdracht.Status)
            {
                statusChanged = true;
                if (onderhoudsopdracht.Status == Common.OnderhoudStatus.Klaargemeld && onderhoudsopdracht.IsApk)
                {
                    klaargemeldEvent = true;
                }else if (onderhoudsopdracht.Status == Common.OnderhoudStatus.Klaargemeld && !onderhoudsopdracht.IsApk)
                {
                    onderhoudsopdracht.Status = Common.OnderhoudStatus.Afgemeld;
                }
            }

            oldOnderhoudsopdracht.Status = onderhoudsopdracht.Status;
            oldOnderhoudsopdracht.Kilometerstand = onderhoudsopdracht.Kilometerstand;
            oldOnderhoudsopdracht.IsApk = onderhoudsopdracht.IsApk;
            oldOnderhoudsopdracht.AutoId = onderhoudsopdracht.AutoId;
            oldOnderhoudsopdracht.Werkzaamheden = onderhoudsopdracht.Werkzaamheden;
            oldOnderhoudsopdracht.SteekproefDatum = onderhoudsopdracht.SteekproefDatum;

            _onderhoudRepository.Update(oldOnderhoudsopdracht);

            if (statusChanged)
            {
                OnderhoudStatusChangedEvent onderhoudStatusChangedEvent = CreateOnderhoudStatusChangedEvent(onderhoudsopdracht);
                _publisher.Publish(onderhoudStatusChangedEvent);
            }
            if (klaargemeldEvent)
            {
                AutoKlaargemeldEvent autoKlaargemeldEvent = CreateAutoKlaargemeldEvent(onderhoudsopdracht);
                _publisher.Publish(autoKlaargemeldEvent);
            }

            return onderhoudsopdracht;
        }


        private AutoCreatedEvent CreateAutoCreatedEvent(Auto auto)
        {
            return new AutoCreatedEvent()
            {
                GUID = Guid.NewGuid().ToString(),
                RoutingKey = "Jomaya.Auto.Created",
                TimeStamp = DateTime.UtcNow,
                AutoId = auto.Id,
                Kenteken = auto.Kenteken,
                KlantId = auto.KlantId,
                Type = auto.Type
            };
        }

        private OnderhoudsopdrachtCreatedEvent CreateOnderhoudsopdrachtCreatedEvent(Onderhoudsopdracht onderhoudsopdracht)
        {
            return new OnderhoudsopdrachtCreatedEvent() {
                GUID = Guid.NewGuid().ToString(),
                RoutingKey = "Jomaya.Onderhoudsopdracht.Created",
                TimeStamp = DateTime.UtcNow,
                Status = onderhoudsopdracht.Status,
                OnderhoudsopdrachtId = onderhoudsopdracht.Id,
                AutoId = onderhoudsopdracht.AutoId,
                Kilometerstand = onderhoudsopdracht.Kilometerstand,
                SteekproefDatum = onderhoudsopdracht.SteekproefDatum,
                IsApk = onderhoudsopdracht.IsApk,
                Werkzaamheden = onderhoudsopdracht.Werkzaamheden
            };
        }
        private OnderhoudStatusChangedEvent CreateOnderhoudStatusChangedEvent(Onderhoudsopdracht onderhoudsopdracht)
        {
            return new OnderhoudStatusChangedEvent()
            {
                GUID = Guid.NewGuid().ToString(),
                RoutingKey = "Jomaya.Onderhoudsopdracht.Updated",
                TimeStamp = DateTime.UtcNow,
                Status = onderhoudsopdracht.Status,
                OnderhoudsopdrachtId = onderhoudsopdracht.Id,
                AutoId = onderhoudsopdracht.AutoId,
                Kilometerstand = onderhoudsopdracht.Kilometerstand,
                SteekproefDatum = onderhoudsopdracht.SteekproefDatum,
                IsApk = onderhoudsopdracht.IsApk,
                Werkzaamheden = onderhoudsopdracht.Werkzaamheden
            };
        }
        private AutoKlaargemeldEvent CreateAutoKlaargemeldEvent(Onderhoudsopdracht onderhoudsopdracht)
        {
            return new AutoKlaargemeldEvent()
            {
                GUID = Guid.NewGuid().ToString(),
                RoutingKey = "Jomaya.Onderhoudsopdracht.Updated",
                TimeStamp = DateTime.UtcNow,
                OnderhoudId = onderhoudsopdracht.Id,
                AutoId = onderhoudsopdracht.AutoId,
                KilometerStand = onderhoudsopdracht.Kilometerstand,
                IsApk = onderhoudsopdracht.IsApk,
                Werkzaamheden = onderhoudsopdracht.Werkzaamheden,
                VoertuigType = (Common.VoertuigTypes)Enum.Parse(typeof(Common.VoertuigTypes), onderhoudsopdracht.Auto.Type),
                Kenteken = onderhoudsopdracht.Auto.Kenteken,
                EigenaarNaam = ""
                                
            };
        }
        public void Dispose()
        {
            _autoRepository?.Dispose();
        }
    }
}
