using System;

namespace ParkingLot.Domain.Slots.Exceptions
{
    public class ParkingLotFullException : Exception
    {
        public override string Message => "Sorry, parking lot is full";
    }
}
