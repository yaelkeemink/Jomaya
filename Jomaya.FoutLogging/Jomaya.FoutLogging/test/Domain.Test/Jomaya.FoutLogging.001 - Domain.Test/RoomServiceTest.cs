using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.WSA.Commons;
using Moq;

namespace Jomaya.FoutLogging._001___Domain.Test {
    [TestClass]
    public class RoomServiceTest 
    {
        [TestMethod]
        public void StartGameProcessesCommandTest() 
        {
            // Arrange
            //var repositoryMock = new Mock<IRepository<Room, long>>(MockBehavior.Strict);
            //var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            //repositoryMock.Setup(x => x.Insert(It.IsAny<Room>())).Returns(1);
            //publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            //var target = new RoomService(repositoryMock.Object, publisherMock.Object);
            //var createRoomCommand = new CreateRoomCommand();

            //// Act
            //target.StartGame(createRoomCommand);

            //// Assert
            //repositoryMock.Verify(x => x.Insert(It.IsAny<Room>()), Times.Once());
            //publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());
        }

        [TestMethod]
        public void StartGameRoomSameNameAsGameRoomCommandTest() {
            // Arrange
            //var repositoryMock = new Mock<IRepository<Room, long>>(MockBehavior.Strict);
            //var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            //repositoryMock.Setup(x => x.Insert(It.IsAny<Room>())).Returns(1);
            //publisherMock.Setup(x => x.Publish(It.IsAny<DomainEvent>()));

            //var target = new RoomService(repositoryMock.Object, publisherMock.Object);
            //var createRoomCommand = new CreateRoomCommand() { RoomName = "Chess-01" };

            //// Act
            //var result = target.StartGame(createRoomCommand);

            //// Assert
            //repositoryMock.Verify(x => x.Insert(It.IsAny<Room>()), Times.Once());
            //publisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Once());

            //Assert.IsNotNull(result);
            //Assert.AreEqual(createRoomCommand.RoomName, result.Name);
        }

        [TestMethod]
        public void EndGameRunningIsFalseTest() {
            // Arrange
            //var repositoryMock = new Mock<IRepository<Room, long>>(MockBehavior.Strict);
            //var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            //var endCommand = new EndGameCommand() { RoomId = 1 };
            //var foundRoom = new Room() { Id = 1, Name = "Chess-01", Running = true };

            //repositoryMock.Setup(x => x.Find(1)).Returns(foundRoom);
            //repositoryMock.Setup(x => x.Update(foundRoom)).Returns(1);

            //var target = new RoomService(repositoryMock.Object, publisherMock.Object);
            //var createRoomCommand = new CreateRoomCommand() { RoomName = "Chess-01" };

            //// Act
            //var result = target.EndGame(endCommand);

            //// Assert
            //repositoryMock.Verify(x => x.Find(endCommand.RoomId), Times.Once());
            //repositoryMock.Verify(x => x.Update(It.IsAny<Room>()), Times.Once());

            //Assert.IsNotNull(result);
            //Assert.AreEqual(foundRoom.Name, result.Name);
            //Assert.IsFalse(result.Running);
        }
    }
}
