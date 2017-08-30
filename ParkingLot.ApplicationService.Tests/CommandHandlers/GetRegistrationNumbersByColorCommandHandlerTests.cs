using System;
using System.Collections.Generic;
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
    public class GetRegistrationNumbersByColorCommandHandlerTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetRegistrationNumbersByColorCommandHandler commandHandler =
                new GetRegistrationNumbersByColorCommandHandler(mockedSlotmanager, mockedScreenWriter);

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
            Assert.Throws<ArgumentNullException>(() => new GetRegistrationNumbersByColorCommandHandler(null, null));
        }

        [Fact]
        public void ExecuteMustCallProperMethod()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            mockedSlotmanager.GetCarsRegistrationNumber(null).ReturnsForAnyArgs(new [] {"L1234", "L1235"});
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetRegistrationNumbersByColorCommandHandler commandHandler =
                new GetRegistrationNumbersByColorCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new GetRegistrationNumbersByColorCommand("White");

            // Act
            commandHandler.Execute(command);

            // Assert
            mockedSlotmanager.ReceivedWithAnyArgs().GetCarsRegistrationNumber(new CarHavingColor("White"));
            mockedScreenWriter.ReceivedWithAnyArgs().WriteLine(null);
        }

        [Fact]
        public void ExecuteMustCallWithEmptyQueryResultWillThrowException()
        {
            // Arrange
            ICarSlotManager mockedSlotmanager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetRegistrationNumbersByColorCommandHandler commandHandler =
                new GetRegistrationNumbersByColorCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new GetRegistrationNumbersByColorCommand("White");

            // Act
            // Assert
            Assert.Throws<CarNotFoundException>(() => commandHandler.Execute(command));
        }
    }
}
