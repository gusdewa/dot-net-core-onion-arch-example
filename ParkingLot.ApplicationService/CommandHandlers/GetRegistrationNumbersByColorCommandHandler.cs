using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.ApplicationService.Commands;
using ParkingLot.ApplicationService.Exceptions;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService.CommandHandlers
{
    public class GetRegistrationNumbersByColorCommandHandler : ICommandHandler
    {
        private readonly IScreenWriter _screenWriter;
        private readonly ICarSlotManager _slotManager;

        public GetRegistrationNumbersByColorCommandHandler(ICarSlotManager slotManager, IScreenWriter screenWriter)
        {
            _slotManager = slotManager ?? throw new ArgumentNullException();
            _screenWriter = screenWriter ?? throw new ArgumentNullException();
        }

        public void Execute(ICommand command)
        {
            GetRegistrationNumbersByColorCommand concreteCommand = command as GetRegistrationNumbersByColorCommand ??
                                                                   throw new InvalidOperationException();
            
            IEnumerable<string> registrationNumbers = _slotManager.GetCarsRegistrationNumber(concreteCommand.QuerySpecification);
            if (!registrationNumbers.Any())
            {
                throw new CarNotFoundException();
            }

            string commaSeparatedResult =
                registrationNumbers.Aggregate("", (acc, cur) => string.IsNullOrEmpty(acc) ? cur :$"{acc}, {cur}");
            _screenWriter.WriteLine(commaSeparatedResult);
        }
    }
}