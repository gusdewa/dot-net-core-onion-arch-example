using System;

namespace ParkingLot.Domain.Cars.Exceptions
{
    public class NegativeOrZeroSlotNumberException : Exception
    {
        public override string Message => "Slot number must be greater than 0";
    }
}