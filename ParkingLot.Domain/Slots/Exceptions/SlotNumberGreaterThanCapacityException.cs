using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Slots.Exceptions
{
    public class SlotNumberGreaterThanCapacityException : Exception
    {
        public override string Message => "Slot number must not greater than parking lot capacity";
    }
}
