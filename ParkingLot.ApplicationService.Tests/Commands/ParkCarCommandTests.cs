using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.Commands
{
    public class ParkCarCommandTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            ParkCarCommand command = new ParkCarCommand("L-1234", "White");
            // Act
            // Assert
            Assert.NotNull(command);
        }
    }
}
