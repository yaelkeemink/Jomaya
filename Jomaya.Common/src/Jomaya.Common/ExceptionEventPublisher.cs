using Jomaya.Common.Events;
using Minor.WSA.Commons;
using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Common
{
    public class ExceptionEventPublisher
    {

        public static void PublishException(Exception e, bool inDockerContainer = true)
        {
            string  eventBusHost;
            int eventBusPort;

            if (inDockerContainer)
            {
                eventBusHost = "rabbitmq";
                eventBusPort = 5672;
            }
            else
            {
                eventBusHost = "localhost";
                eventBusPort = 5673;
            }

            var config = new EventBusConfig()
            {
                Host = eventBusHost,
                Port = eventBusPort,
                QueueName = "jomaya.exception.publisher",
                ExchangeName = "my-bus"
            };

            var myEvent = new ExceptionThrownEvent()
            {
                ExceptionType = e.GetType(),
                GUID = Guid.NewGuid().ToString(),
                Message = e.Message,
                RoutingKey = "jomaya.exception.exceptionthrownevent",
                StackTrace = e.StackTrace,
                TimeStamp = DateTime.Now
            };

            using (var publisher = new EventPublisher(config))
            {
                publisher.Publish(myEvent);
            }
            
        }
    }
}
