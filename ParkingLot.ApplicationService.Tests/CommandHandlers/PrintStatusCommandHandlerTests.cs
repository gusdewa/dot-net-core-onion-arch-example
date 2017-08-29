using System;
using NSubstitute;
using ParkingLot.ApplicationService.CommandHandlers;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.CommandHandlers
{
    public class PrintStatusCommandHandlerTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            PrintStatusCommandHandler commandHandler = new PrintStatusCommandHandler(mockedSlotmanager, mockedScreenWriter);

            // Act
            // Assert
            Assert.NotNull(commandHandler);
        }
        
        [Fact]
        public void InvalidContractorMustThrowException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new PrintStatusCommandHandler(null, null));
        }

        [Fact]
        public void ExecuteMustCallProperMethod()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            PrintStatusCommandHandler commandHandler = new PrintStatusCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new PrintStatusCommand();
            
            // Act
            commandHandler.Execute(command);

            // Assert
            mockedSlotmanager.Received().GenerateStatusReport();
        }
    }
}
