using System;
using System.Text;
using ParkingLot.Domain.Cars;
using Xunit;

namespace ParkingLot.Domain.Tests.Cars
{
    public class CarTests
    {
        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            const string validRegistrationNumber = "DOES NOT MATTER";
            const string validColor = "DOES NOT MATTER EITHER";
            Car car = new Car(validRegistrationNumber, validColor);

            // Act
            // Assert
            Assert.NotNull(car);
        }
        
        [Fact]
        public void InvalidContractorMustThrowException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new Car(null, null));
        }

        [Fact]
        public void GenerateReportMustReturnCorrectReport()
        {
            // Arrange
            const string validRegistrationNumber = "L-1234-XX";
            const string validColor = "White";
            Car car = new Car(validRegistrationNumber, validColor);
            car.Park(1);
            StringBuilder expectedReport = new StringBuilder($"1\t{validRegistrationNumber}\t{validColor}");

            // Act
            StringBuilder actualReport = car.GenerateStatusReport();

            // Assert
            Assert.True(expectedReport.Equals(actualReport));
        }

        [Fact]
        public void EqualityMustBasedOnRegistrationNumberAndColor()
        {
            // Arrange
            const string color = "White";
            const string registrationNumber = "AAAAAAA";
            Car car = new Car(registrationNumber, color);
            Car identicalCar = new Car(registrationNumber, color);
            Car differentCarForRegistrationNumber = new Car("BBBBBBB", color);
            Car differentCarForColor = new Car(registrationNumber, "Black");
            Car differentCar = new Car("BBBBBBB", "Black");
            Car parkedIdenticalCar = new Car(registrationNumber, color);
            parkedIdenticalCar.Park(1);

            // Act
            // Assert
            Assert.True(car.Equals(identicalCar));
            Assert.True(car.Equals(parkedIdenticalCar));
            Assert.False(car.Equals(differentCar));
            Assert.False(car.Equals(differentCarForColor));
            Assert.False(car.Equals(differentCarForRegistrationNumber));
        }
    }
}
