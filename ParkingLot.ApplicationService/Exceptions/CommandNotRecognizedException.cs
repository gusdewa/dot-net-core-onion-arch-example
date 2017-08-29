using System;

namespace ParkingLot.ApplicationService.Exceptions
{
    public class CommandNotRecognizedException : Exception
    {
        public override string Message => "Command name is not recognized";
    }
}
