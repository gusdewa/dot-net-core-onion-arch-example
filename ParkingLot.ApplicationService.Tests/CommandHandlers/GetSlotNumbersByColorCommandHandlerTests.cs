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
    public class GetSlotNumbersByColorCommandHandlerTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetSlotNumbersByColorCommandHandler commandHandler =
                new GetSlotNumbersByColorCommandHandler(mockedSlotmanager, mockedScreenWriter);

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
            Assert.Throws<ArgumentNullException>(() => new GetSlotNumbersByColorCommandHandler(null, null));
        }

        [Fact]
        public void ExecuteMustCallProperMethod()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            mockedSlotmanager.GetCarsSlotNumber(null).ReturnsForAnyArgs(new[] {1, 2});
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetSlotNumbersByColorCommandHandler commandHandler =
                new GetSlotNumbersByColorCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new GetSlotNumbersByColorCommand("White");

            // Act
            commandHandler.Execute(command);

            // Assert
            mockedSlotmanager.ReceivedWithAnyArgs().GetCarsSlotNumber(new CarHavingColor("White"));
            mockedScreenWriter.ReceivedWithAnyArgs().WriteLine(null);
        }

        [Fact]
        public void ExecuteMustCallWithEmptyQueryResultWillThrowException()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetSlotNumbersByColorCommandHandler commandHandler =
                new GetSlotNumbersByColorCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new GetSlotNumbersByColorCommand("White");

            // Act
            // Assert
            Assert.Throws<CarNotFoundException>(() => commandHandler.Execute(command));
        }
    }
}
