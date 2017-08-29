using System;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService.CommandHandlers
{
    public class LeaveCarCommandHandler : ICommandHandler
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public LeaveCarCommandHandler(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public void Execute(ICommand command)
        {
            LeaveCarCommand concreteCommand = command as LeaveCarCommand ?? throw new InvalidOperationException();
            _slotManager.TakeCarOut(concreteCommand.SlotNumber);
            _screenWriter.WriteLine(concreteCommand.FeedbackMessage);
        }
    }
}