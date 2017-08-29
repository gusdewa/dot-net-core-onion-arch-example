using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.Commands
{
    public class LeaveCarCommandTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            LeaveCarCommand command = new LeaveCarCommand(1);
            // Act
            // Assert
            Assert.NotNull(command);
        }
    }
}
