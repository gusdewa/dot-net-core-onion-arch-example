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
            StringBuilder expectedReport = new StringBuilder($"{validRegistrationNumber}\t{validColor}");

            // Act
            StringBuilder actualReport = car.GenerateStatusReport();

            // Assert
            Assert.True(expectedReport.Equals(actualReport));
        }
    }
}
