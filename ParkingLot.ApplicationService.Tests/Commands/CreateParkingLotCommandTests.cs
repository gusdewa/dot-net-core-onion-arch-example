using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.Commands
{
    public class CreateParkingLotCommandTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            CreateParkingLotCommand command = new CreateParkingLotCommand(20);
            // Act
            // Assert
            Assert.NotNull(command);
        }
    }
}
