using System;
using System.Collections.Generic;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Slots;
using Xunit;

namespace ParkingLot.Domain.Tests.Slots
{
    public class SlotManagerTests
    {
        private IDictionary<int, Car> GenerateDummyParkingLot(int max = 5) => new Dictionary<int, Car>(max);

        [Fact]
        public void ValidContractorMustInstantiate()
        {
            // Arrange
            IDictionary<int, Car> carRepository = GenerateDummyParkingLot();
            // Act
            SlotManager slotManager = new SlotManager(carRepository);
            // Assert
            Assert.NotNull(slotManager);
        }

        [Fact]
        public void InvalidContractorMustThrowException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new SlotManager(null));
        }

        /*
        int GenerateNewFreeSlotNumber();
        IEnumerable<Car> GetCars(ICarSpecification carQuerySpecification);
        int GetFreeSlotNumber();
        IEnumerable<int> GetSlotNumbers(ICarSpecification carQuerySpecification);
        bool IsSlotFree(int slotNumber);
        void Leave(int slotNumber);
        int Park(Car car);
        */

        [Fact]
        public void GenerateNewFreeSlotNumberMustReturnFirstIntegerWhenInitiallyCreated()
        {
            // Arrange
            IDictionary<int, Car> carRepository = GenerateDummyParkingLot();
            SlotManager slotManager = new SlotManager(carRepository);
            // Act
            int firstSlotNumber = slotManager.GenerateNewFreeSlotNumber();
            // Assert
            Assert.Equal(1, firstSlotNumber);
        }
    }
}
