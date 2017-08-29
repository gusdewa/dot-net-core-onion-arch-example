using System;
using System.Collections.Generic;
using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests
{
    public class CommandFactoryTests
    {
        public static IEnumerable<object[]> MappedTypeData => new[]
        {
            new object[] {"park", typeof(ParkCarCommand)},
            new object[] {"leave", typeof(LeaveCarCommand)},
            new object[] {"create_parking_lot", typeof(CreateParkingLotCommand)},
            new object[] {"status", typeof(PrintStatusCommand)},
            new object[] {"registration_numbers_for_cars_with_colour", typeof(GetRegistrationNumbersByColorCommand)},
            new object[] {"slot_number_for_registration_number", typeof(GetSlotNumberByRegistrationNumberCommand)},
            new object[] {"slot_numbers_for_cars_with_colour", typeof(GetSlotNumbersByColorCommand)}
        };


        [Theory, MemberData(nameof(MappedTypeData))]
        public void CreateMustProduceProperInstance(string name, Type type)
        {
            // Arrange
            CommandFactory factory = new CommandFactory();
            // Act
            ICommand command = factory.Create(name, new[] {"1", "2"});
            // Assert
            Assert.Equal(type, command.GetType());
        }
    }
}
