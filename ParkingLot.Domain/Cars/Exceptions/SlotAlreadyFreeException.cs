using System;

namespace ParkingLot.Domain.Cars.Exceptions
{
    public class SlotAlreadyFreeException : Exception
    {
        public override string Message => "Slot is already free";
    }
}