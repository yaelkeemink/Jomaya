using Jomaya.Klantenservice.Entities;
using Jomaya.Klantenservice.Services;
using Jomaya.Klantenservice.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.WSA.Commons;
using Moq;

namespace Jomaya.Klantenservice._001___Domain.Test
{
    [TestClass]
    public class RoomServiceTest 
    {
        [TestMethod]
        public void StartGameProcessesCommandTest() 
        {
            // Arrange
            var repositoryMock = new Mock<IRepository<Klant, long>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            repositoryMock.Setup(x => x.Insert(It.IsAny<Klant>())).Returns(1);
            publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            var target = new KlantService(repositoryMock.Object, publisherMock.Object);
            var klant = new Klant();

            // Act
            target.CreateKlant(klant);

            // Assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<Klant>()), Times.Once());
            publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());
        }

        [TestMethod]
        public void StartGameRoomSameNameAsGameRoomCommandTest() {
            // Arrange
            var repositoryMock = new Mock<IRepository<Klant, long>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            repositoryMock.Setup(x => x.Insert(It.IsAny<Klant>())).Returns(1);
            publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            var target = new KlantService(repositoryMock.Object, publisherMock.Object);
            var klant = new Klant() { Voorletters = "YP", Achternaam = "Keemink" };

            // Act
            var result = target.CreateKlant(klant);

            // Assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<Klant>()), Times.Once());
            publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());

            Assert.IsNotNull(result);
            Assert.AreEqual(klant.Voorletters, result.Voorletters);
            Assert.AreEqual(klant.Achternaam, result.Achternaam);
        }        
    }
}
