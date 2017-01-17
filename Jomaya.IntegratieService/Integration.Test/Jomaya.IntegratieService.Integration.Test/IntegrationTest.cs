using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Publisher;
using Microsoft.Extensions.Configuration;
using Jomaya.IntegratieService.Facade.Dispatchers;
using Jomaya.IntegratieService.Services;
using System.Threading;
using Jomaya.Common.Events;
using System;
using System.IO;

namespace Jomaya.IntegratieService.Integration.Test
{
    [TestClass]
    public class IntegrationTest
    {
        public IConfigurationRoot Configuration { get; }

        [TestMethod]
        public void DispatcherGetsEventAndConnectToRDWAndGetAResponse()
        {
            var _configTo = new EventBusConfig()
            {
                ExchangeName = "jomaya.eventbus",
                Host = Configuration.GetConnectionString("RabbitMQ") ?? "localhost",
                Port = 5673,
                QueueName = "DispatcherGetsEventAndConnectToRDWAndGetAResponseTo"
            };

            var _configFrom = new EventBusConfig()
            {
                ExchangeName = "jomaya.eventbus",
                Host = Configuration.GetConnectionString("RabbitMQ") ?? "localhost",
                Port = 5673,
                QueueName = "DispatcherGetsEventAndConnectToRDWAndGetAResponseFrom"
            };
            var filePath = "integratie_test.txt";

            using (var eventSender = new EventPublisher(_configFrom))
            using (var testStarter = new EventPublisher(_configTo))
            using (var rdwDispatcher = new RDWDispatcher(_configTo, new RDWService(eventSender)))
            using (var testReceiver = new TestDispatcher(_configFrom))
            {
                rdwDispatcher.LogFilePath = filePath;
                AutoKlaargemeldEvent aae = new AutoKlaargemeldEvent()
                {
                    AutoId = 1,
                    Kenteken = "AB-BA-33",
                    GUID = Guid.NewGuid().ToString(),
                    RoutingKey = "",
                    TimeStamp = DateTime.UtcNow,
                    KilometerStand = 1234,
                    VoertuigType = (int)Common.VoertuigTypes.Personenauto,
                    IsApk = true,
                    KlantId = 1,
                    Werkzaamheden = "test",
                    OnderhoudId = 1,
                    OnderhoudsDatum = DateTime.UtcNow
                };
                rdwDispatcher.Uri = new Uri("http://localhost:4003/");

                testStarter.Publish(aae);

                Thread.Sleep(2000);

                Assert.AreEqual(2, testReceiver.ReceivedEventCount);
                Assert.AreEqual("Jomaya.Common.Events.APKKeuringsregistratieEvent" , testReceiver.ReceivedEvent.BasicProperties.Type);
            }
        }

        [TestMethod]
        public void DispatcherLogsRDWRequests()
        {
            var _config = new EventBusConfig()
            {
                Host = "localhost",
                Port = 5673,
                QueueName = "DispatcherLogsRDWRequests"                
            };

            var filePath = "test.txt";
            using (var eventSender = new EventPublisher(_config))
            using (var rdwDispatcher = new RDWDispatcher(_config, new RDWService(eventSender)))
            {
                rdwDispatcher.LogFilePath = filePath;
                rdwDispatcher.Uri = new Uri("http://localhost:4003/");
                var myEvent = new AutoKlaargemeldEvent()
                {
                    AutoId = 1,
                    GUID = "TESTGUID",
                    Kenteken = "TE-ST-55",
                    IsApk = true,
                    KilometerStand = 123456,
                    RoutingKey = "Niks",
                    TimeStamp = DateTime.Now,
                    VoertuigType = (int)Common.VoertuigTypes.Personenauto,
                    KlantId = 1,
                };


                eventSender.Publish(myEvent);

                Thread.Sleep(5000);

                var logPath = Path.GetTempFileName();
                var result = File.ReadAllText(filePath);

                var expectedFirstpart = $"TimeStamp: {myEvent.TimeStamp} Guid: TESTGUID AutoID: 1 Eigenaar: ";
                var expectedLastPart = $" Kenteken: TE-ST-55 KilometerStand: 123456 VoertuigType: 0 ";

                Assert.IsTrue(result.Contains(expectedFirstpart));
                Assert.IsTrue(result.Contains(expectedLastPart));
                File.Delete(filePath);
            }
        }
    }
}
