using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.ApplicationService.Exceptions;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService.CommandHandlers
{
    public class GetSlotNumberByRegistrationNumberCommandHandler : ICommandHandler
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public GetSlotNumberByRegistrationNumberCommandHandler(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public void Execute(ICommand command)
        {
            GetSlotNumberByRegistrationNumberCommand concreteCommand =
                command as GetSlotNumberByRegistrationNumberCommand ??
                throw new InvalidOperationException();

            IEnumerable<int> slotNumbers = _slotManager.GetCarsSlotNumber(concreteCommand.QuerySpecification);
            if (!slotNumbers.Any())
            {
                throw new CarNotFoundException();
            }

            string commaSeparatedResult =
                slotNumbers.Aggregate("", (acc, cur) => string.IsNullOrEmpty(acc) ? cur + "" : $"{acc}, {cur}");
            _screenWriter.WriteLine(commaSeparatedResult);
        }
    }
}