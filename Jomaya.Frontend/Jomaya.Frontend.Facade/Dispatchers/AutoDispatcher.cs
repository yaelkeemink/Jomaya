using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jomaya.Common.Events;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Jomaya.Frontend.Entities;
using Jomaya.Common;
using Jomaya.Frontend.Services.Interfaces;

namespace Jomaya.Frontend.Facade.Dispatchers
{
    [RoutingKey("#")]
    public class AutoDispatcher : EventDispatcher
    {
        private IRepository<Auto, long> _autoRepo;
        private IRepository<Onderhoudsopdracht, long> _onderhoudRepo;
        public AutoDispatcher(EventBusConfig config, IRepository<Auto, long> autoRepo, IRepository<Onderhoudsopdracht, long> onderhoudRepo)
            : base(config)
        {
            _autoRepo = autoRepo;
            _onderhoudRepo = onderhoudRepo;
        }

        public void OnAutoCreated(AutoCreatedEvent incomingEvent)
        {
            try
            {
                _autoRepo.Insert(new Auto()
                {
                    Id = incomingEvent.AutoId,
                    Kenteken = incomingEvent.Kenteken,
                    KlantId = incomingEvent.KlantId,
                    Type = incomingEvent.Type,
                });
            }
            catch(Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
            }
        }
        public void OnOnderhoudCreated(OnderhoudsopdrachtCreatedEvent incomingEvent)
        {
            try
            {
                _onderhoudRepo.Insert(new Onderhoudsopdracht()
                {
                    Id = incomingEvent.OnderhoudsopdrachtId,
                    AutoId = incomingEvent.AutoId,
                    Datum = incomingEvent.OnderhoudsDatum,
                    IsApk = incomingEvent.IsApk,
                    Kilometerstand = incomingEvent.Kilometerstand,
                    Status = (OnderhoudStatus)incomingEvent.Status,
                    SteekproefDatum = incomingEvent.SteekproefDatum,
                    Werkzaamheden = incomingEvent.Werkzaamheden,
                });
            }
            catch(Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
            }
        }
        public void OnOnderhoudStatusChanged(OnderhoudStatusChangedEvent incomingEvent)
        {
            try
            {
                var opdracht = _onderhoudRepo.Find(incomingEvent.OnderhoudsopdrachtId);
                opdracht.Status = (OnderhoudStatus)incomingEvent.Status;
                _onderhoudRepo.Update(opdracht);
            }
            catch(Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
            }
        }
        }
}
