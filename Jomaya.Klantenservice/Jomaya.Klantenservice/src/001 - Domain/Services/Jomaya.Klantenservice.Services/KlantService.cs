using System;
using Minor.WSA.Commons;
using Jomaya.Klantenservice.Entities;
using Jomaya.Common.Events;
using Jomaya.Klantenservice.Services.Interfaces;

namespace Jomaya.Klantenservice.Services
{
    public class KlantService : IDisposable
    {
        private readonly IRepository<Klant, long> _repository;
        private readonly IEventPublisher _publisher;

        public KlantService(IRepository<Klant, long> repository, IEventPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public Klant CreateKlant(Klant klant)
        {            
            _repository.Insert(klant);

            // throw KlantCreatedEvent
            var klantCreatedEvent = new KlantCreatedEvent()
            {
                GUID = Guid.NewGuid().ToString(),
                RoutingKey = "Jomaya.Klant.KlantCreated",
                TimeStamp = DateTime.UtcNow,
                KlantId = klant.Id,
                Voorletters = klant.Voorletters,
                Tussenvoegsel = klant.Tussenvoegsel,
                Achternaam = klant.Achternaam,
                Telefoonnummer = klant.Telefoonnummer,                
            };
            _publisher.Publish(klantCreatedEvent);

            return klant;
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }

        public Klant GetKlant(long klantId)
        {
            Klant klant = _repository.Find(klantId);
            string eigenaarNaam = $"{klant.Voorletters} {klant.Tussenvoegsel} {klant.Achternaam}";
            return klant;
        }
    }
}
