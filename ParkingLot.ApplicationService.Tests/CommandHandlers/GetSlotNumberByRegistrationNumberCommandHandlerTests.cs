using System;
using NSubstitute;
using ParkingLot.ApplicationService.CommandHandlers;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.ApplicationService.Exceptions;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Cars.Specifications;
using ParkingLot.Domain.Utils;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.CommandHandlers
{
    public class GetSlotNumberByRegistrationNumberCommandHandlerTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetSlotNumberByRegistrationNumberCommandHandler commandHandler =
                new GetSlotNumberByRegistrationNumberCommandHandler(mockedSlotmanager, mockedScreenWriter);

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
            Assert.Throws<ArgumentNullException>(() => new GetSlotNumberByRegistrationNumberCommandHandler(null, null));
        }

        [Fact]
        public void ExecuteMustCallProperMethod()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            mockedSlotmanager.GetCarsSlotNumber(null).ReturnsForAnyArgs(new[] {1, 2});
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetSlotNumberByRegistrationNumberCommandHandler commandHandler =
                new GetSlotNumberByRegistrationNumberCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new GetSlotNumberByRegistrationNumberCommand("L-123");

            // Act
            commandHandler.Execute(command);

            // Assert
            mockedSlotmanager.ReceivedWithAnyArgs().GetCarsSlotNumber(new CarHavingRegistrationNumber("L-123"));
            mockedScreenWriter.ReceivedWithAnyArgs().WriteLine(null);
        }

        [Fact]
        public void ExecuteMustCallWithEmptyQueryResultWillThrowException()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetSlotNumberByRegistrationNumberCommandHandler commandHandler =
                new GetSlotNumberByRegistrationNumberCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new GetSlotNumberByRegistrationNumberCommand("White");

            // Act
            // Assert
            Assert.Throws<CarNotFoundException>(() => commandHandler.Execute(command));
        }
    }
}
