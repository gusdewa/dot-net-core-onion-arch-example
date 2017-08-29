using System.Collections.Generic;
using ParkingLot.Domain.Cars.Specifications;
using ParkingLot.Domain.Reports;

namespace ParkingLot.Domain.Cars
{
    public interface ICarSlotManager : IStatusReportPrintable
    {
        void CreateParkingLot(int maxCapacity);
        int GenerateNewFreeSlotNumber();
        IEnumerable<Car> GetCars();
        IEnumerable<string> GetCarsColor(ICarSpecification carQuerySpecification);
        IEnumerable<string> GetCarsRegistrationNumber(ICarSpecification carQuerySpecification);
        IEnumerable<int> GetCarsSlotNumber(ICarSpecification carQuerySpecification);
        int GetFreeSlotNumber();
        void TakeCarOut(int slotNumber);
        int PutCarIn(Car car);
    }
}