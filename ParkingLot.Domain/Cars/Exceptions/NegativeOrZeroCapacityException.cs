using System;

namespace ParkingLot.Domain.Cars.Exceptions
{
    public class NegativeOrZeroCapacityException : Exception
    {
        public override string Message => "Parking lot capacity must be greater than 0";
    }
}