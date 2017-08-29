using System;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService.CommandHandlers
{
    public class CreateParkingLotCommandHandler : ICommandHandler
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public CreateParkingLotCommandHandler(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public void Execute(ICommand command)
        {
            CreateParkingLotCommand concreteCommand =
                command as CreateParkingLotCommand ?? throw new InvalidOperationException();
            _slotManager.CreateParkingLot(concreteCommand.MaxCapacity);
            _screenWriter.WriteLine(concreteCommand.FeedbackMessage);
        }
    }
}