using Minor.WSA.EventBus.Dispatcher;
using Minor.WSA.EventBus.Config;

namespace Jomaya.Klantenservice.Facade.Dispatchers
{
    [RoutingKey("#")]
    public class KlantDispatcher : EventDispatcher 
    {
        public KlantDispatcher(EventBusConfig config) : base(config) 
        {

        }

        //public void OnKlantCreated(IncomingEvent incomingEvent) 
        //{
        //    // Do whatever you want with incoming event (persist or process)
        //}
    }
}
