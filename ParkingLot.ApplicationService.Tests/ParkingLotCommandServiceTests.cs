using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using ParkingLot.ApplicationService.Exceptions;
using ParkingLot.Domain.Utils;
using Xunit;

namespace ParkingLot.ApplicationService.Tests
{
    public class ParkingLotCommandServiceTests
    {
        [Fact]
        public void RegisterMustAddCommandToQueue()
        {
            // Arrange
            IScreenWriter writer = Substitute.For<IScreenWriter>();
            IParkingLotCommandService service = new ParkingLotCommandService(writer);
            Assert.Equal(0, service.GetRegisteredCommands().Count());
            // Act
            service.Register("park", new[] {"L-1234", "White"});
            // Assert
            Assert.Equal(1, service.GetRegisteredCommands().Count());
        }

        [Fact]
        public void RegisterUnknownCommandMustThrowException()
        {
            // Arrange
            IScreenWriter writer = Substitute.For<IScreenWriter>();
            IParkingLotCommandService service = new ParkingLotCommandService(writer);
            // Act
            // Assert
            Assert.Throws<CommandNotRecognizedException>(() => service.Register("random", new[] {"L-1234", "White"}));
        }

        [Fact]
        public void RegisterAllMustAddAllCommandsToQueue()
        {
            // Arrange
            IScreenWriter writer = Substitute.For<IScreenWriter>();
            IParkingLotCommandService service = new ParkingLotCommandService(writer);
            string stringFromTextFile = $"leave 1\n" +
                                        $"park L-1234 White\n" +
                                        $"park L-1235 Brown\n";
            IEnumerable<KeyValuePair<string, string[]>> commandArgs = service.ExtractCommandStatements(stringFromTextFile);
            
            // Act
            service.RegisterAll(commandArgs);

            // Assert
            Assert.Equal(3, service.GetRegisteredCommands().Count());
        }

        [Fact]
        public void ExtractCommandStatementMustProduceListOfNameAndArgs()
        {
            // Arrange
            IScreenWriter writer = Substitute.For<IScreenWriter>();
            IParkingLotCommandService service = new ParkingLotCommandService(writer);

            string stringFromTextFile = $"leave 1\n" +
                                        $"park L-1234 White\n" +
                                        $"park L-1235 Brown\n";

            // Act
            IEnumerable<KeyValuePair<string, string[]>> commandArgs = service.ExtractCommandStatements(stringFromTextFile);

            // Assert
            Assert.Equal(3,  commandArgs.Count());
        }

        [Fact]
        public void ExecuteMustExecuteFistActionAndDequeue()
        {
            // Arrange
            IScreenWriter writer = Substitute.For<IScreenWriter>();
            IParkingLotCommandService service = new ParkingLotCommandService(writer);
            service.Register("create_parking_lot", new []{ "10"});
            Assert.Equal(1, service.GetRegisteredCommands().Count());

            // Act
            service.Execute();

            // Assert
            Assert.Equal(0, service.GetRegisteredCommands().Count());
        }

        [Fact]
        public void ExecuteAllMustExecuteAllActionsUntilQueueEmpty()
        {
            // Arrange
            IScreenWriter writer = Substitute.For<IScreenWriter>();
            IParkingLotCommandService service = new ParkingLotCommandService(writer);
            service.Register("create_parking_lot", new []{ "10"});
            service.Register("park", new []{ "L-1234", "White"});
            service.Register("park", new []{ "L-1235", "Brown"});
            service.Register("park", new []{ "L-1236", "Yellow"});
            Assert.Equal(4, service.GetRegisteredCommands().Count());

            // Act
            service.ExecuteAll();

            // Assert
            Assert.False(service.GetRegisteredCommands().Any());
        }
    }
}
