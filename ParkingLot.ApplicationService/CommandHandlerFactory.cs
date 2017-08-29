using System;
using ParkingLot.ApplicationService.CommandHandlers;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService
{
    public class CommandHandlerFactory
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public CommandHandlerFactory(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public ICommandHandler Create(string name)
        {
            switch (name.Trim().ToLower())
            {
                case "park": return new ParkCarCommandHandler(_slotManager, _screenWriter);
                case "leave": return new LeaveCarCommandHandler(_slotManager, _screenWriter);
                case "create_parking_lot": return new CreateParkingLotCommandHandler(_slotManager, _screenWriter);
                case "status": return new PrintStatusCommandHandler(_slotManager, _screenWriter);
                case "registration_numbers_for_cars_with_colour": return new GetRegistrationNumbersByColorCommandHandler(_slotManager, _screenWriter);
                case "slot_number_for_registration_number": return new GetSlotNumberByRegistrationNumberCommandHandler(_slotManager, _screenWriter);
                case "slot_numbers_for_cars_with_colour": return new GetSlotNumbersByColorCommandHandler(_slotManager, _screenWriter);
                default: return null;
            }
        }
    }
}