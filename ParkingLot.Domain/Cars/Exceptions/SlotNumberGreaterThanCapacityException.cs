using System;

namespace ParkingLot.Domain.Cars.Exceptions
{
    public class SlotNumberGreaterThanCapacityException : Exception
    {
        public override string Message => "Slot number must not greater than parking lot capacity";
    }
}