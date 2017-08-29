﻿using System;
using System.Linq;
using ParkingLot.ApplicationService.Commands;
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
            var slotNumbers = _slotManager.GetCarsSlotNumber(concreteCommand.QuerySpecification);
            string commaSeparatedResult =
                slotNumbers.Aggregate("", (acc, cur) => string.IsNullOrEmpty(acc) ? cur + "" : $", {cur}");
            _screenWriter.WriteLine(commaSeparatedResult);
        }
    }
}