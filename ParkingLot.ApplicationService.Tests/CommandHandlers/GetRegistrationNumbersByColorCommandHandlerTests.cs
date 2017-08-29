using System;
using NSubstitute;
using ParkingLot.ApplicationService.CommandHandlers;
using ParkingLot.ApplicationService.Commands;
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
            IScreenWriter mockedScreenWriter = Substitute.For<IScreenWriter>();
            GetRegistrationNumbersByColorCommandHandler commandHandler =
                new GetRegistrationNumbersByColorCommandHandler(mockedSlotmanager, mockedScreenWriter);
            ICommand command = new GetRegistrationNumbersByColorCommand("White");

            // Act
            commandHandler.Execute(command);

            // Assert
            mockedSlotmanager.ReceivedWithAnyArgs().GetCarsRegistrationNumber(new CarHavingColor("White"));
        }
    }
}
