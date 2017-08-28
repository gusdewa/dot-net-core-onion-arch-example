using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Slots.Exceptions
{
    public class NegativeOrZeroCapacityException : Exception
    {
        public override string Message => "Parking lot capacity must be greater than 0";
    }
}
