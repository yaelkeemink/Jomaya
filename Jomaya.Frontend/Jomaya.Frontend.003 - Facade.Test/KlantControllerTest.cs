using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Facade.Controllers;
using Jomaya.Frontend.Infrastructure.Agents;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend._003___Facade.Test
{
    [TestClass]
    public class KlantControllerTest
    { 
        [TestMethod]
        public void InvoerenLevertEenViewResultTest()
        {
            // Arrange           
            var agentMock = new Mock<IKlantServiceAgent>(MockBehavior.Strict);
            var target = new KlantController(agentMock.Object);

            // Act
            var result = target.Invoeren();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ToevoegenLevertEenViewResultEnCalledAutoServiceAgentTest()
        {
            // Arrange           
            var agentMock = new Mock<IKlantServiceAgent>(MockBehavior.Strict);
            var klant = new Klant()
            {
                Voorletters = "Y.P",
                Achternaam = "Keemink",
            };
            var returnKlant = new Klant()
            {
                Id = 1,
                Voorletters = "Y.P",
                Achternaam = "Keemink",
            };
            agentMock.Setup(agent => agent.KlantInvoeren(It.IsAny<Klant>())).Returns(returnKlant);
            var target = new KlantController(agentMock.Object);

            // Act
            var result = target.Insert(klant);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            agentMock.Verify(agent => agent.KlantInvoeren(It.IsAny<Klant>()), Times.Once());
        }
    }
}
