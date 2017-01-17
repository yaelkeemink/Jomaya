using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Dispatcher;
using Jomaya.Common.Events;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Infrastructure.Agents.KlantService.Models;
using System;
using Jomaya.Common;
using Jomaya.Frontend.Services.Interfaces;

namespace Jomaya.Frontend.Facade.Dispatchers
{
    [RoutingKey("#")]
    public class KlantDispatcher : EventDispatcher
    {
        private IRepository<Entities.Klant, long> _autoRepo;
        public KlantDispatcher(EventBusConfig config, IRepository<Entities.Klant, long> autoRepo)
            : base(config)
        {
            _autoRepo = autoRepo;
        }

        public void OnKlantCreated(KlantCreatedEvent incomingEvent)
        {
            try
            {
                _autoRepo.Insert(new Entities.Klant()
                {
                    Id = incomingEvent.KlantId,
                    Voorletters = incomingEvent.Voorletters,
                    Tussenvoegsel = incomingEvent.Tussenvoegsel,
                    Achternaam = incomingEvent.Achternaam,
                    Telefoonnummer = incomingEvent.Telefoonnummer,
                });
            }
            catch(Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
            }
        }
    }
}