using System;
using ParkingLot.ApplicationService.Commands;

namespace ParkingLot.ApplicationService
{
    public class CommandFactory
    {
        public ICommand Create(string name, string[] args)
        {
            switch (name.Trim().ToLower())
            {
                case "park": return new ParkCarCommand(args[0], args[1]);
                case "leave": return new LeaveCarCommand(Convert.ToInt32(args[0]));
                case "create_parking_lot": return new CreateParkingLotCommand(Convert.ToInt32(args[0]));
                case "status": return new PrintStatusCommand();
                case "registration_numbers_for_cars_with_colour": return new GetRegistrationNumbersByColorCommand(args[0]);
                case "slot_number_for_registration_number": return new GetSlotNumberByRegistrationNumberCommand(args[0]);
                case "slot_numbers_for_cars_with_colour": return new GetSlotNumbersByColorCommand(args[0]);
                default: return null;
            }
        }
    }
}