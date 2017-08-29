using System;

namespace ParkingLot.Domain.Cars.Exceptions
{
    public class ParkingLotFullException : Exception
    {
        public override string Message => "Sorry, parking lot is full";
    }
}