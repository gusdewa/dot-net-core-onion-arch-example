using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Cars.Specifications;
using Xunit;

namespace ParkingLot.Domain.Tests.Cars.Specifications
{
    public class CarHavingRegistrationNumberTests
    {
        [Fact]
        public void CarWithSameRegistrationNumberMustSatisfy()
        {
            // Arrange
            const string registrationNumber = "KA-01-HH-3141";
            Car car = new Car(registrationNumber, "Whatever Color");
            ICarSpecification specification = new CarHavingRegistrationNumber(registrationNumber);

            // Act
            // Assert
            Assert.True(specification.IsSatisfiedBy(car));
        }

        [Fact]
        public void CarWithDifferentRegistrationNumberMustFail()
        {
            // Arrange
            const string registrationNumber = "KA-01-HH-3141";
            Car car = new Car(registrationNumber, "Whatever Color");
            ICarSpecification specification = new CarHavingRegistrationNumber("Not KA-01-HH-3141");

            // Act
            // Assert
            Assert.False(specification.IsSatisfiedBy(car));
        }
    }
}
