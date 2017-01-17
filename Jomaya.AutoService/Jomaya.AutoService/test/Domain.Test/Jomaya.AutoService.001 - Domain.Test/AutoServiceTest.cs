using Jomaya.AutoService.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.WSA.Commons;
using Moq;
using Jomaya.Common;
using Jomaya.AutoService.Services.Interfaces;
using Jomaya.AutoService.Infrastructure.DAL;

namespace Jomaya.AutoService._001___Domain.Test
{
    [TestClass]
    public class AutoServiceTest 
    {
        [TestMethod]
        public void CreateAutoProcessesCommandTest() 
        {
            // Arrange
            var autoRepositoryMock = new Mock<IRepository<Auto, long>>(MockBehavior.Strict);
            var onderhoudRepositoryMock = new Mock<IRepository<Onderhoudsopdracht, long>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            autoRepositoryMock.Setup(x => x.Insert(It.IsAny<Auto>())).Returns(1);
            publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            var target = new Services.AutoService(autoRepositoryMock.Object, onderhoudRepositoryMock.Object, publisherMock.Object);
            //var createAutoCommand = new CreateAutoCommand();

            // Act
            target.CreateAuto(new Auto() { KlantId = 1, Kenteken = "AA-BB-12" });

            // Assert
            autoRepositoryMock.Verify(x => x.Insert(It.IsAny<Auto>()), Times.Once());
            publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());
        }

        [TestMethod]
        public void CreateAutoSameNameAsCreateAutoCommandTest() {
            // Arrange
            var autoRepositoryMock = new Mock<IRepository<Auto, long>>(MockBehavior.Strict);
            var onderhoudRepositoryMock = new Mock<IRepository<Onderhoudsopdracht, long>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            autoRepositoryMock.Setup(x => x.Insert(It.IsAny<Auto>())).Returns(1);
            publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            var target = new Jomaya.AutoService.Services.AutoService(autoRepositoryMock.Object, onderhoudRepositoryMock.Object, publisherMock.Object);
           
            // Act
            var result = target.CreateAuto(new Auto() { KlantId = 1, Kenteken = "AA-BB-12" });

            // Assert
            autoRepositoryMock.Verify(x => x.Insert(It.IsAny<Auto>()), Times.Once());
            publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());

            Assert.IsNotNull(result);
            Assert.AreEqual("AA-BB-12", result.Kenteken);
        }

        [TestMethod]
        public void CreateOnderhoudsopdrachtProcessesCommandTest()
        {
            // Arrange
            var autoRepositoryMock = new Mock<IRepository<Auto, long>>(MockBehavior.Strict);
            var onderhoudRepositoryMock = new Mock<IRepository<Onderhoudsopdracht, long>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            autoRepositoryMock.Setup(x => x.Find(It.IsAny<long>())).Returns(new Auto() { Id = 1});
            onderhoudRepositoryMock.Setup(x => x.Insert(It.IsAny<Onderhoudsopdracht>())).Returns(1);
            publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            var target = new Services.AutoService(autoRepositoryMock.Object, onderhoudRepositoryMock.Object, publisherMock.Object);
            
            // Act
            target.CreateOnderhoudsopdracht(new Onderhoudsopdracht() { Kilometerstand = 1234 });

            // Assert
            onderhoudRepositoryMock.Verify(x => x.Insert(It.IsAny<Onderhoudsopdracht>()), Times.Once());
            publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());
        }

        [TestMethod]
        public void UpdateOnderhoudsopdrachtProcessesCommandTest()
        {
            // Arrange
            var autoRepositoryMock = new Mock<IRepository<Auto, long>>(MockBehavior.Strict);
            var onderhoudRepositoryMock = new Mock<IRepository<Onderhoudsopdracht, long>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            onderhoudRepositoryMock.Setup(x => x.Find(It.IsAny<long>())).Returns(new Onderhoudsopdracht() { Id = 1 });
            onderhoudRepositoryMock.Setup(x => x.Update(It.IsAny<Onderhoudsopdracht>())).Returns(1);
            publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            var target = new Services.AutoService(autoRepositoryMock.Object, onderhoudRepositoryMock.Object, publisherMock.Object);
            
            // Act
            target.UpdateOnderhoudsopdracht(new Onderhoudsopdracht() { Id =  1, Status = OnderhoudStatus.Opgepakt });

            // Assert
            onderhoudRepositoryMock.Verify(x => x.Find(It.IsAny<long>()), Times.Once());
            onderhoudRepositoryMock.Verify(x => x.Update(It.IsAny<Onderhoudsopdracht>()), Times.Once());
            publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());
        }
    }
}
