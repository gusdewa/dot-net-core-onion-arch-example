using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.ApplicationService.Exceptions;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService.CommandHandlers
{
    public class GetSlotNumbersByColorCommandHandler : ICommandHandler
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public GetSlotNumbersByColorCommandHandler(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public void Execute(ICommand command)
        {
            GetSlotNumbersByColorCommand concreteCommand =
                command as GetSlotNumbersByColorCommand ?? throw new InvalidOperationException();
            IEnumerable<int> slotNumbers = _slotManager.GetCarsSlotNumber(concreteCommand.QuerySpecification);
            IEnumerable<int> enumerable = slotNumbers as int[] ?? slotNumbers.ToArray();
            if (!enumerable.Any())
            {
                throw new CarNotFoundException();
            }

            string commaSeparatedResult =
                enumerable.Aggregate("", (acc, cur) => string.IsNullOrEmpty(acc) ? cur + "" : $"{acc}, {cur}");
            _screenWriter.WriteLine(commaSeparatedResult);
        }
    }
}