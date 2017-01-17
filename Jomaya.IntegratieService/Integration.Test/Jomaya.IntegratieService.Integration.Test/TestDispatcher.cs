using System;
using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Dispatcher;
using Minor.WSA.Commons;
using Jomaya.Common.Events;
using RabbitMQ.Client.Events;

namespace Jomaya.IntegratieService.Integration.Test
{
    [RoutingKey("#")]
    public class TestDispatcher : EventDispatcher
    {
        public int ReceivedEventCount { get; private set; }
        public BasicDeliverEventArgs ReceivedEvent { get; private set; }

        public TestDispatcher(EventBusConfig config) : base(config)
        {
        }
        
        protected override void EventReceived(object sender, BasicDeliverEventArgs e)
        {
            ReceivedEventCount++;
            ReceivedEvent = e;
        }
    }
}