using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Cars.Specifications;
using ParkingLot.Domain.Reports;
using ParkingLot.Domain.Slots.Exceptions;

namespace ParkingLot.Domain.Slots
{
    public class SlotManager : ISlotManager, IStatusReport
    {
        /// <summary>
        /// Repository for cars with a unique slot number representing a parking lot
        /// </summary>
        private readonly IDictionary<int, Car> _carSlots;

        public SlotManager(IDictionary<int, Car> carSlots)
        {
            _carSlots = carSlots ?? throw new ArgumentNullException(); // Dependency injected through constructor
        }

        /// <summary>
        /// Get the first free (missing) slot number from a positive unique list of slot numbers.
        /// Complexity: O(n)
        /// </summary>
        /// <returns>The firstly-found free slot number. If not found, return 0.</returns>
        public int GetFreeSlotNumber()
        {
            int total = _carSlots.Keys.Sum();
            return _carSlots.Keys.Aggregate(total, (current, occupiedSlotNumber) => current - occupiedSlotNumber);
        }

        /// <summary>
        /// Get the next free slot number as long as within parking lot capacity
        /// </summary>
        /// <returns>The next free slot number</returns>
        public int GenerateNewFreeSlotNumber()
        {
            if (!_carSlots.Any()) return 1;

            int nextFreeSlotNumber= _carSlots.Keys.Max() + 1;
            if (nextFreeSlotNumber > _carSlots.Count)
            {
                throw new ParkingLotFullException();
            }
            return nextFreeSlotNumber;
        }

        /// <summary>
        /// Check if a given slot number has no car parked in
        /// </summary>
        /// <param name="slotNumber">Slot number to check</param>
        /// <returns>True if no car parked</returns>
        public bool IsSlotFree(int slotNumber) => !_carSlots.ContainsKey(slotNumber);
        
        /// <summary>
        /// Put a new coming car to the parking lot
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public int Park(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException();
            }

            int freeSlotNumber = GetFreeSlotNumber();
            if (freeSlotNumber == 0)
            {
                freeSlotNumber = GenerateNewFreeSlotNumber();
            }
            _carSlots.Add(freeSlotNumber, car);
            return freeSlotNumber;
        }

        /// <summary>
        /// Take the car parked in a given slot number out from the parking lot
        /// </summary>
        /// <param name="slotNumber">A slot number the car must exit</param>
        public void Leave(int slotNumber)
        {
            if (slotNumber <= 0)
            {
                throw new NegativeOrZeroSlotNumberException(); 
            }
            if (slotNumber > _carSlots.Count)
            {
                throw new SlotNumberGreaterThanCapacityException();
            }
            if (IsSlotFree(slotNumber))
            {
                throw new SlotAlreadyFreeException();
            }
            _carSlots.Remove(slotNumber);
        }

        /// <summary>
        /// Get all slot numbers matched with the given car specification
        /// </summary>
        /// <param name="carQuerySpecification">A car specification to find</param>
        /// <returns>Slot numbers matched</returns>
        public IEnumerable<int> GetSlotNumbers(ICarSpecification carQuerySpecification)
        {
            return _carSlots
                .Where(e => carQuerySpecification.IsSatisfiedBy(e.Value))
                .Select(e => e.Key);
        }

        /// <summary>
        /// Get all cars that matched with the given car specification
        /// </summary>
        /// <param name="carQuerySpecification">A car specification to find</param>
        /// <returns>Cars matched</returns>
        public IEnumerable<Car> GetCars(ICarSpecification carQuerySpecification)
        {
            return _carSlots
                .Where(e => carQuerySpecification.IsSatisfiedBy(e.Value))
                .Select(e => e.Value);
        }

        /// <summary>
        /// Generate tab-delimited status report including header, no, and car data
        /// </summary>
        /// <returns>Tab-delimited status report</returns>
        public StringBuilder GenerateStatusReport()
        {
            StringBuilder status = new StringBuilder();
            status.AppendLine($"No\tRegistration Slot No.\tColour");
            foreach (KeyValuePair<int, Car> carSlot in _carSlots)
            {
                status.AppendLine($"{carSlot.Key}\t{carSlot.Value.GenerateStatusReport()}\t");                
            }
            return status;
        }
    }
}
