using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.Commands
{
    public class GetSlotNumberByRegistrationNumberCommandTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            GetSlotNumberByRegistrationNumberCommand command = new GetSlotNumberByRegistrationNumberCommand("L-1234");
            // Act
            // Assert
            Assert.NotNull(command);
        }
    }
}
