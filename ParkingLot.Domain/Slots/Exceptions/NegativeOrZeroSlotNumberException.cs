using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Slots.Exceptions
{
    public class NegativeOrZeroSlotNumberException : Exception
    {
        public override string Message => "Slot number must be greater than 0";
    }
}
