using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Cars.Exceptions;
using ParkingLot.Domain.Cars.Specifications;
using Xunit;

namespace ParkingLot.Domain.Tests.Cars
{
    public class CarSlotManagerTests
    {
        private Car[] _dummyCars = {
            new Car("AAAAA", "White"),
            new Car("BBBBB", "Brown"),
            new Car("CCCCC", "White"),
            new Car("AAAAA", "Red"),
            new Car("EEEEE", "White"),
        };

        private const int MAX_CAPACITY = 5;

        private ICarSlotManager GeneratePrepopulatedSlotManager(int numberOfPrepopulation = MAX_CAPACITY)
        {
            CarSlotManager carSlotManager = new CarSlotManager(new Dictionary<int, Car>(), MAX_CAPACITY);
            for (int carIndex = 0; carIndex < numberOfPrepopulation; carIndex++)
            {
                carSlotManager.PutCarIn(_dummyCars[carIndex]);
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
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);
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

        [Fact]
        public void PutCarInMustAddItemIntoRepo()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(3);
            Car newCar = new Car("NEWBIE", "Brown");
            Assert.Equal(3, slotManager.GetCars().Count());

            // Act
            int assignedSlotNumber = slotManager.PutCarIn(newCar);

            // Assert
            Assert.Equal(4, slotManager.GetCars().Count());
            Assert.Contains(newCar, slotManager.GetCars());
        }

        [Fact]
        public void PutCarInMustAddItemAssignedToTheFirstSlotNumberWhenEmpty()
        {
            // Arrange
            ICarSlotManager slotManager = GenerateEmptySlotManager();
            Car newCar = new Car("NEWBIE", "Brown");
            
            // Act
            int assignedSlotNumber = slotManager.PutCarIn(newCar);

            // Assert
            Assert.Equal(1, assignedSlotNumber);
        }

        [Fact]
        public void PutCarInMustThrowExceptionIfItsBeenFull()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);
            Car newCar = new Car("NEWBIE", "Brown");
            
            // Act
            // Assert
            Assert.Throws<ParkingLotFullException>(() => slotManager.PutCarIn(newCar));
        }

        [Fact]
        public void TakeCarOutMustRemoveCarFromRepo()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(3);
            Assert.Equal(3, slotManager.GetCars().Count());

            // Act
            slotManager.TakeCarOut(1);

            // Assert
            Assert.Equal(2, slotManager.GetCars().Count());
            Assert.False(slotManager.GetCars().Any(e => e.Equals(_dummyCars[0])));
        }

        [Fact]
        public void TakeCarOutWithNegativeParameterMustThrowException()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(3);

            // Act
            // Arrange
            Assert.Throws<NegativeOrZeroSlotNumberException>(() => slotManager.TakeCarOut(int.MinValue));
        }

        [Fact]
        public void TakeCarOutWithSlotNumberGreaterThanCapacityMustThrowException()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);

            // Act
            // Arrange
            Assert.Throws<SlotNumberGreaterThanCapacityException>(() => slotManager.TakeCarOut(int.MaxValue));
        }

        [Fact]
        public void TakeCarOutWithSlotNumberThatIsFreeAlreadyMustThrowException()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);
            slotManager.TakeCarOut(3);

            // Act
            // Arrange
            Assert.Throws<SlotAlreadyFreeException>(() => slotManager.TakeCarOut(3));
        }

        [Fact]
        public void GetCarsMustReturnAllCars()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(3);

            // Act
            IEnumerable<Car> allCars = slotManager.GetCars();

            // Assert
            Assert.Equal(3, allCars.Count());
            Assert.Contains(_dummyCars[0], allCars);
            Assert.Contains(_dummyCars[1], allCars);
            Assert.Contains(_dummyCars[2], allCars);
        }

        [Fact]
        public void GetCarsColorByRegistrationNumberMustReturnCarsColor()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);
            ICarSpecification lookedUpCarSpecification = new CarHavingRegistrationNumber("AAAAA");

            // Act
            IEnumerable<string> matchedCarsColor = slotManager.GetCarsColor(lookedUpCarSpecification);
            // Arrange
            Assert.Equal(2, matchedCarsColor.Count());
            Assert.Contains("Red", matchedCarsColor);
            Assert.Contains("White", matchedCarsColor);
        }
        
        [Fact]
        public void GetCarsRegistrationNumberByColorMustReturnCarsRegistrationNumber()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);
            ICarSpecification lookedUpCarSpecification = new CarHavingColor("White");

            // Act
            IEnumerable<string> matchedCarsRegistrationNumbers = slotManager.GetCarsRegistrationNumber(lookedUpCarSpecification);
            // Arrange
            Assert.Equal(3, matchedCarsRegistrationNumbers.Count());
            Assert.Contains("AAAAA", matchedCarsRegistrationNumbers);
            Assert.Contains("CCCCC", matchedCarsRegistrationNumbers);
            Assert.Contains("EEEEE", matchedCarsRegistrationNumbers);
        }

        [Fact]
        public void GetCarsSlotNumberByRegistrationNumberMustReturnCarsSlotNumber()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);
            ICarSpecification lookedUpCarSpecification = new CarHavingRegistrationNumber("AAAAA");

            // Act
            IEnumerable<int> matchedSlotNumbers = slotManager.GetCarsSlotNumber(lookedUpCarSpecification);
            // Arrange
            Assert.Equal(2, matchedSlotNumbers.Count());
            Assert.Contains(1, matchedSlotNumbers);
            Assert.Contains(4, matchedSlotNumbers);
        }
        
        [Fact]
        public void GetCarsSlotNumberByColorMustReturnCarsSlotNumber()
        {
            // Arrange
            ICarSlotManager slotManager = GeneratePrepopulatedSlotManager(MAX_CAPACITY);
            ICarSpecification lookedUpCarSpecification = new CarHavingColor("White");

            // Act
            IEnumerable<int> matchedSlotNumbers = slotManager.GetCarsSlotNumber(lookedUpCarSpecification);
            // Arrange
            Assert.Equal(3, matchedSlotNumbers.Count());
            Assert.Contains(1, matchedSlotNumbers);
            Assert.Contains(3, matchedSlotNumbers);
            Assert.Contains(5, matchedSlotNumbers);
        }
    }
}
