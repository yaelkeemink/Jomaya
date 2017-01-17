using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Facade.Controllers;
using Jomaya.Frontend.Infrastructure.Agents.Service;
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
    public class AutoControllerTest
    {
        [TestMethod]
        public void InvoerenLevertEenViewResultTest()
        {
            // Arrange           
            var agentMock = new Mock<IAutoServiceAgent>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.AutoInvoeren(It.IsAny<Auto>())).Returns(new Auto());
            var target = new AutoController(agentMock.Object);
            var insertedKlantID = 1;

            // Act
            var result = target.Invoeren(insertedKlantID);
            var auto = (result as ViewResult).Model as Auto;
           
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(auto.KlantId, insertedKlantID);
        }


        [TestMethod]
        public void ToevoegenLevertEenViewResultEnCalledAutoServiceAgentTest()
        {
            // Arrange           
            var agentMock = new Mock<IAutoServiceAgent>(MockBehavior.Strict);
            var auto = new Entities.Auto() { KlantId = 1, Kenteken = "AA-BB-11" };
            var returnAuto = new Entities.Auto() { Id = 1, Type = "personenauto", KlantId = 1, Kenteken = "AA-BB-11" };
            agentMock.Setup(agent => agent.AutoInvoeren(It.IsAny<Auto>())).Returns(returnAuto);
            var target = new AutoController(agentMock.Object);

            // Act
            var result = target.Toevoegen(auto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            agentMock.Verify(agent => agent.AutoInvoeren(It.IsAny<Auto>()), Times.Once());
        }
    }
}
