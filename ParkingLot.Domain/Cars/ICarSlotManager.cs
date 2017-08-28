using System.Collections.Generic;
using ParkingLot.Domain.Cars.Specifications;

namespace ParkingLot.Domain.Cars
{
    public interface ICarSlotManager
    {
        int GenerateNewFreeSlotNumber();
        IEnumerable<string> GetCarsColor(ICarSpecification carQuerySpecification);
        IEnumerable<string> GetCarsRegistrationNumber(ICarSpecification carQuerySpecification);
        IEnumerable<int> GetCarsSlotNumber(ICarSpecification carQuerySpecification);
        int GetFreeSlotNumber();
        void TakeCarOut(int slotNumber);
        int PutCarInto(Car car);
    }
}