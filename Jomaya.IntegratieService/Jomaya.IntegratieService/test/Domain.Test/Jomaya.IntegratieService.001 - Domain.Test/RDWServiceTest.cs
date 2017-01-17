using Jomaya.Common;
using Jomaya.Common.Events;
using Jomaya.IntegratieService.Entities;
using Jomaya.IntegratieService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.WSA.Commons;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jomaya.IntegratieService._001___Domain.Test
{
    [TestClass]
    public class RDWServiceTest
    {
        [TestMethod]
        public void CreateMessagePublishedEventOfRDWResponse()
        {
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            publisherMock.Setup(pub => pub.Publish(It.IsAny<DomainEvent>()));
            var testEvent = new AutoKlaargemeldEvent() { VoertuigType = (int)VoertuigTypes.Personenauto, Kenteken = "AB-BA-22", KilometerStand = 1234, EigenaarNaam = "J. jansen", AutoId = 1, GUID = Guid.NewGuid().ToString(), RoutingKey = "", TimeStamp = DateTime.UtcNow };
            Garage testGarage = new Garage() { GarageNaam = "Jomaya", PlaatsNaam = "Utrecht" };

            using (var target = new RDWService(publisherMock.Object))
            {
                target.Createmessage(testEvent, testGarage);

                Thread.Sleep(1000);

                publisherMock.Verify(pub => pub.Publish(It.IsAny<DomainEvent>()), Times.Once());
            }
        }
    }
}
