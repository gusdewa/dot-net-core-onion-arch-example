using System;
using System.Text;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService.CommandHandlers
{
    public class PrintStatusCommandHandler : ICommandHandler
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public PrintStatusCommandHandler(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public void Execute(ICommand command)
        {
            StringBuilder result = _slotManager.GenerateStatusReport();
            if (result != null)
            {
                _screenWriter.WriteLine(result.ToString());
            }
        }
    }
}