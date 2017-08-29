using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.Commands
{
    public class PrintStatusCommandTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            PrintStatusCommand command = new PrintStatusCommand();
            // Act
            // Assert
            Assert.NotNull(command);
        }
    }
}
