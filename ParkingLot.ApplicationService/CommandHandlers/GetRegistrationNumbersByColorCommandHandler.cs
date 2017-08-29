using System;
using System.Linq;
using ParkingLot.ApplicationService.Commands;
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
            var registrationNumbers = _slotManager.GetCarsRegistrationNumber(concreteCommand.QuerySpecification);
            string commaSeparatedResult =
                registrationNumbers.Aggregate("", (acc, cur) => string.IsNullOrEmpty(acc) ? cur : $", {cur}");
            _screenWriter.WriteLine(commaSeparatedResult);
        }
    }
}