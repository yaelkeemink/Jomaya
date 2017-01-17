using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Facade.Controllers;
using Jomaya.Frontend.Infrastructure.Agents.Service;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Jomaya.Frontend.Services.Interfaces;
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
    public class OnderhoudControllerTest
    {
        [TestMethod]
        public void InvoerenLevertEenViewResultTest()
        {
            // Arrange           
            var agentMock = new Mock<IAutoServiceAgent>(MockBehavior.Strict);
            var repoMock = new Mock<IRepository<Onderhoudsopdracht, long>>(MockBehavior.Strict);
            repoMock.Setup(repo => repo.Insert(It.IsAny<Onderhoudsopdracht>())).Returns(1);
            var autoRepoMock = new Mock<IRepository<Auto, long>>(MockBehavior.Strict);
            autoRepoMock.Setup(repo => repo.Insert(It.IsAny<Auto>())).Returns(1);
            var klantRepoMock = new Mock<IRepository<Klant, long>>(MockBehavior.Strict);
            klantRepoMock.Setup(repo => repo.Insert(It.IsAny<Klant>())).Returns(1);
            var target = new OnderhoudController(agentMock.Object, autoRepoMock.Object, repoMock.Object, klantRepoMock.Object);
            var auto = new Auto() { Kenteken = "AA-BB-12", Type = "personenauto", KlantId =  1, Id =  1 };

            // Act
            var result = target.Invoeren(auto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ToevoegenLevertEenViewResultEnCalledAutoServiceAgentTest()
        {
            // Arrange           
            var agentMock = new Mock<IAutoServiceAgent>(MockBehavior.Strict);
            var repoMock = new Mock<IRepository<Onderhoudsopdracht, long>>(MockBehavior.Strict);
            repoMock.Setup(repo => repo.Insert(It.IsAny<Onderhoudsopdracht>())).Returns(1);
            var autoRepoMock = new Mock<IRepository<Auto, long>>(MockBehavior.Strict);
            autoRepoMock.Setup(repo => repo.Insert(It.IsAny<Auto>())).Returns(1);
            var klantRepoMock = new Mock<IRepository<Klant, long>>(MockBehavior.Strict);
            klantRepoMock.Setup(repo => repo.Insert(It.IsAny<Klant>())).Returns(1);
            var onderhoudsopdracht = new Entities.Onderhoudsopdracht() { Auto = new Auto() { KlantId = 1, Kenteken = "AA-BB-11" } };
            var returnOnderhoudsopdracht = new Entities.Onderhoudsopdracht() { Id = 1, Auto = new Auto() { KlantId = 1, Kenteken = "AA-BB-11", Type = "personenauto" } };
            agentMock.Setup(agent => agent.OnderhoudsopdrachtInvoeren(It.IsAny<Onderhoudsopdracht>())).Returns(returnOnderhoudsopdracht);
            var target = new OnderhoudController(agentMock.Object, autoRepoMock.Object, repoMock.Object, klantRepoMock.Object);

            // Act
            var result = target.Toevoegen(onderhoudsopdracht);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            agentMock.Verify(agent => agent.OnderhoudsopdrachtInvoeren(It.IsAny<Onderhoudsopdracht>()), Times.Once());
        }
    }
}
