using System;

namespace ParkingLot.ApplicationService.Exceptions
{
    public class CarNotFoundException : Exception
    {
        public override string Message => "Not found";
    }
}
