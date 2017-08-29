using System;
using System.Collections.Generic;
using NSubstitute;
using ParkingLot.ApplicationService.CommandHandlers;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;
using Xunit;

namespace ParkingLot.ApplicationService.Tests
{
    public class CommandHandlerFactoryTests
    {
        public static IEnumerable<object[]> MappedTypeData => new[]
        {
            new object[] {"park", typeof(ParkCarCommandHandler)},
            new object[] {"leave", typeof(LeaveCarCommandHandler)},
            new object[] {"create_parking_lot", typeof(CreateParkingLotCommandHandler)},
            new object[] {"status", typeof(PrintStatusCommandHandler)},
            new object[] {"registration_numbers_for_cars_with_colour", typeof(GetRegistrationNumbersByColorCommandHandler)},
            new object[] {"slot_number_for_registration_number", typeof(GetSlotNumberByRegistrationNumberCommandHandler)},
            new object[] {"slot_numbers_for_cars_with_colour", typeof(GetSlotNumbersByColorCommandHandler)}
        };

        [Theory, MemberData(nameof(MappedTypeData))]
        public void CreateMustProduceProperInstance(string name, Type type)
        {
            // Arrange
            ICarSlotManager mockedManager = Substitute.For<ICarSlotManager>();
            IScreenWriter mockedWriter = Substitute.For<IScreenWriter>();
            CommandHandlerFactory factory = new CommandHandlerFactory(mockedManager, mockedWriter);
            // Act
            ICommandHandler commandHandler = factory.Create(name);
            // Assert
            Assert.Equal(type, commandHandler.GetType());
        }
    }
}
