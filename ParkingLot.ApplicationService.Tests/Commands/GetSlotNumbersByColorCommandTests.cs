using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.Commands
{
    public class GetSlotNumbersByColorCommandTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            GetSlotNumbersByColorCommand command = new GetSlotNumbersByColorCommand("White");
            // Act
            // Assert
            Assert.NotNull(command);
        }
    }
}
