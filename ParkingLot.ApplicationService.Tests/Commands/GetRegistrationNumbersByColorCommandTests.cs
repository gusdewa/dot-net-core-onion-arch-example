using ParkingLot.ApplicationService.Commands;
using Xunit;

namespace ParkingLot.ApplicationService.Tests.Commands
{
    public class GetRegistrationNumbersByColorCommandTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            GetRegistrationNumbersByColorCommand command = new GetRegistrationNumbersByColorCommand("White");
            // Act
            // Assert
            Assert.NotNull(command);
        }
    }
}
