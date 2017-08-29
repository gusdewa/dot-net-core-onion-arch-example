using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Cars.Specifications;
using Xunit;

namespace ParkingLot.Domain.Tests.Cars.Specifications
{
    public class CarHavingColorTests
    {
        [Fact]
        public void CarWithSameColorMustSatisfy()
        {
            // Arrange
            const string color = "White";
            Car car = new Car("DOES NOT MATTER", color);
            ICarSpecification specification = new CarHavingColor(color);

            // Act
            // Assert
            Assert.True(specification.IsSatisfiedBy(car));
        }

        [Fact]
        public void CarWithDifferentColorMustFail()
        {
            // Arrange
            const string color = "White";
            Car car = new Car("DOES NOT MATTER", color);
            ICarSpecification specification = new CarHavingColor("Not White");

            // Act
            // Assert
            Assert.False(specification.IsSatisfiedBy(car));
        }
    }
}
