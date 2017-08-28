using System;
using System.Collections.Generic;
using ParkingLot.Domain.Cars;
using Xunit;

namespace ParkingLot.Domain.Tests.Cars
{
    public class CarSlotManagerTests
    {
        private ICarSlotManager GeneratePrepopulatedSlotManager(int numberOfPrepopulation = 5)
        {
            Car[] cars = {
                new Car("AAAAA", "White"),
                new Car("BBBBB", "Brown"),
                new Car("CCCCC", "Yellow"),
                new Car("DDDDD", "Red"),
                new Car("EEEEE", "Green"),
            };

            CarSlotManager carSlotManager = new CarSlotManager(new Dictionary<int, Car>(), 5);
            for (int carIndex = 0; carIndex < numberOfPrepopulation; carIndex++)
            {
                carSlotManager.PutCarInto(cars[carIndex]);
            }
            return carSlotManager;
        }
    
        private ICarSlotManager GenerateEmptySlotManager(int max = 5) => new CarSlotManager(
            new Dictionary<int, Car>(), max);

        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            IDictionary<int, Car> carRepository = new Dictionary<int, Car>();
            const int maxSlotCapacity = 5;

            // Act
            ICarSlotManager slotManager = new CarSlotManager(carRepository, maxSlotCapacity);
            // Assert
            Assert.NotNull(slotManager);
        }

        [Fact]
        public void InvalidContractorMustThrowException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new CarSlotManager(null, 0));
        }

        [Fact]
        public void GenerateNewFreeSlotNumberMustReturnFirstIntegerWhenInitiallyCreated()
        {
            // Arrange
            ICarSlotManager slotManager = GenerateEmptySlotManager();
            // Act
            int firstSlotNumber = slotManager.GenerateNewFreeSlotNumber();
            // Assert
            Assert.Equal(1, firstSlotNumber);
        }

        [Fact]
        public void GenerateNewFreeSlotNumberMustReturnNextFreeSlotNumber()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(3);
            int expectedResult = 4;
            // Act
            int nextFreeSlotNumber = slotManager.GenerateNewFreeSlotNumber();
            // Assert
            Assert.Equal(expectedResult, nextFreeSlotNumber);
        }

        [Fact]
        public void GetFreeSlotNumberMustReturnZeroIfMissingNumberNotFound()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(5);
            // Act
            int freeSlotNumber = slotManager.GetFreeSlotNumber();
            // Assert
            Assert.Equal(0, freeSlotNumber);
        }
        
        [Fact]
        public void GetFreeSlotNumberMustReturnFirstMissingNumber()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(4);
            slotManager.TakeCarOut(2);

            // Act
            int freeSlotNumber = slotManager.GetFreeSlotNumber();
            // Assert
            Assert.Equal(2, freeSlotNumber);
        }
    }
}
