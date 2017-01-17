using Minor.WSA.EventBus.Dispatcher;
using Minor.WSA.EventBus.Config;
using Jomaya.Common.Events;
using Jomaya.AutoService.Entities;

namespace Jomaya.AutoService.Facade.Dispatchers
{
    [RoutingKey("#")]
    public class AutoDispatcher : EventDispatcher 
    {
        private Services.AutoService _autoService;
        public AutoDispatcher(EventBusConfig config, Services.AutoService autoService) : base(config) 
        {
            _autoService = autoService;
        }

        public void OnAPKKeuringsregistratieEventReceived(APKKeuringsregistratieEvent e)
        {
            Onderhoudsopdracht onderhoudsopdracht = CreateOnderhoudsopdrachtFromAPKKeuringsEvent(e);
            _autoService.UpdateOnderhoudsopdracht(onderhoudsopdracht);
        }

        private Onderhoudsopdracht CreateOnderhoudsopdrachtFromAPKKeuringsEvent(APKKeuringsregistratieEvent e)
        {
            // event na RDW Service met steekproef update
            return new Onderhoudsopdracht()
            {
                AutoId = e.AutoId,
                Id = e.OnderhoudId,
                Status = e.Status,
                IsApk = true,
                Kilometerstand = e.Kilometerstand,
                SteekproefDatum = e.SteekproefDatum,
                Werkzaamheden = e.Werkzaamheden,
                Datum = e.OnderhoudsDatum
            };
        }
    }
}
