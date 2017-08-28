using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkingLot.Domain.Cars.Exceptions;
using ParkingLot.Domain.Cars.Specifications;
using ParkingLot.Domain.Reports;

namespace ParkingLot.Domain.Cars
{
    public class CarSlotManager : IStatusReport, ICarSlotManager
    {
        /// <summary>
        ///     Repository for cars with a unique slot number representing a parking lot.
        ///     Dictionary is used to enable indexing for much more efficient look up.
        /// </summary>
        private readonly IDictionary<int, Car> _carSlots;

        private readonly int _maxSlotCapacity;

        public CarSlotManager(IDictionary<int, Car> carSlots, int maxSlotCapacity)
        {
            _carSlots = carSlots ?? throw new ArgumentNullException();
            // Enforce to use existing mechanism to put cars into car slot
            _maxSlotCapacity = maxSlotCapacity;
        }

        /// <summary>
        ///     Generate tab-delimited status report including header, no, and car data
        /// </summary>
        /// <returns>Tab-delimited status report</returns>
        public StringBuilder GenerateStatusReport()
        {
            StringBuilder status = new StringBuilder();
            // Print report header
            status.AppendLine($"No\tRegistration Slot No.\tColour");
            // Print report body (car data)
            foreach (KeyValuePair<int, Car> carSlot in _carSlots)
                status.AppendLine(carSlot.Value.GenerateStatusReport().ToString());
            return status;
        }

        /// <summary>
        ///     Check if a given slot number has no car parked in
        /// </summary>
        /// <param name="slotNumber">Slot number to check</param>
        /// <returns>True if no car parked</returns>
        private bool IsSlotFree(int slotNumber)
        {
            return !_carSlots.ContainsKey(slotNumber);
        }

        /// <summary>
        ///     Get the first free (missing) slot number from the list of slot numbers.
        ///     Look-up Complexity: O(n)
        /// </summary>
        /// <returns>The firstly-found free slot number. If not found, return 0.</returns>
        public int GetFreeSlotNumber()
        {
            // Iterate from 1 to _maxSlotCapacity
            return Enumerable.Range(1, _maxSlotCapacity)
                // return the first slot number which is free
                .FirstOrDefault(IsSlotFree);
        }

        /// <summary>
        ///     Get the next free slot number as long as within parking lot capacity
        /// </summary>
        /// <returns>The next free slot number</returns>
        public int GenerateNewFreeSlotNumber()
        {
            // If no car is parked yet, return 1
            if (!_carSlots.Any()) return 1;

            int nextFreeSlotNumber = _carSlots.Keys.Max() + 1;
            if (nextFreeSlotNumber > _maxSlotCapacity)
                throw new ParkingLotFullException();
            return nextFreeSlotNumber;
        }

        /// <summary>
        ///     Put a new coming car to the parking lot
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public int PutCarInto(Car car)
        {
            if (car == null)
                throw new ArgumentNullException();

            int freeSlotNumber = GetFreeSlotNumber();
            if (freeSlotNumber == 0)
                freeSlotNumber = GenerateNewFreeSlotNumber();

            car.Park(freeSlotNumber);
            _carSlots.Add(freeSlotNumber, car);
            return freeSlotNumber;
        }

        /// <summary>
        ///     Take the car parked in a given slot number out from the parking lot
        /// </summary>
        /// <param name="slotNumber">A slot number the car must exit</param>
        public void TakeCarOut(int slotNumber)
        {
            if (slotNumber <= 0)
                throw new NegativeOrZeroSlotNumberException();
            if (slotNumber > _maxSlotCapacity)
                throw new SlotNumberGreaterThanCapacityException();
            if (IsSlotFree(slotNumber))
                throw new SlotAlreadyFreeException();

            _carSlots[slotNumber].Leave();
            _carSlots.Remove(slotNumber);
        }


        private IEnumerable<Car> GetCars(ICarSpecification carQuerySpecification)
        {
            return _carSlots
                .Where(e => carQuerySpecification.IsSatisfiedBy(e.Value))
                .Select(e => e.Value);
        }

        public IEnumerable<string> GetCarsColor(ICarSpecification carQuerySpecification)
        {
            return GetCars(carQuerySpecification).Select(e => e.Color);
        }

        public IEnumerable<string> GetCarsRegistrationNumber(ICarSpecification carQuerySpecification)
        {
            return GetCars(carQuerySpecification).Select(e => e.RegistrationNumber);
        }

        public IEnumerable<int> GetCarsSlotNumber(ICarSpecification carQuerySpecification)
        {
            return GetCars(carQuerySpecification).Where(e => e.SlotNumber.HasValue).Select(e => e.SlotNumber.Value);
        }
    }
}