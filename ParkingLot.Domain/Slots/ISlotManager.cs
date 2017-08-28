using System.Collections.Generic;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Cars.Specifications;

namespace ParkingLot.Domain.Slots
{
    public interface ISlotManager
    {
        int GenerateNewFreeSlotNumber();
        IEnumerable<Car> GetCars(ICarSpecification carQuerySpecification);
        int GetFreeSlotNumber();
        IEnumerable<int> GetSlotNumbers(ICarSpecification carQuerySpecification);
        bool IsSlotFree(int slotNumber);
        void Leave(int slotNumber);
        int Park(Car car);
    }
}