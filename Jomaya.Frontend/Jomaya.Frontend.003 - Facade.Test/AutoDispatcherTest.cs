using Jomaya.Common.Events;
using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Facade.Controllers;
using Jomaya.Frontend.Facade.Dispatchers;
using Jomaya.Frontend.Infrastructure.Agents.Service;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.WSA.Commons;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend._003___Facade.Test
{
    [TestClass]
    public class AutoDispatcherTest
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            publisherMock.Setup(pub => pub.Publish(It.IsAny<DomainEvent>()));
            var testEvent = new OnderhoudStatusChangedEvent()
            { 
                AutoId = 1,
                IsApk = true,
                Kilometerstand = 1,
                OnderhoudsopdrachtId = 1,
                Werkzaamheden = "",
                GUID = Guid.NewGuid().ToString(),
                RoutingKey = "",
                TimeStamp = DateTime.UtcNow
            };
        }
    }
}
