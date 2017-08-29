using System;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService.CommandHandlers
{
    public class ParkCarCommandHandler : ICommandHandler
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public ParkCarCommandHandler(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public void Execute(ICommand command)
        {
            ParkCarCommand concreteCommand = command as ParkCarCommand ?? throw new InvalidOperationException();
            Car parkingCar = new Car(concreteCommand.RegistrationNumber, concreteCommand.Color);
            int slotAssigned = _slotManager.PutCarIn(parkingCar);
            _screenWriter.WriteLine(string.Format(concreteCommand.FeedbackMessage, slotAssigned));
        }
    }
}