using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Slots.Exceptions
{
    public class SlotAlreadyFreeException : Exception
    {
        public override string Message => "Slot is already free";
    }
}
