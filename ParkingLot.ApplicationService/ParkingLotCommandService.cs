using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.ApplicationService.Exceptions;
using ParkingLot.Domain.Cars;
using ParkingLot.Domain.Utils;

namespace ParkingLot.ApplicationService
{
    public class ParkingLotCommandService : IParkingLotCommandService
    {
        // A queue of deferred actions is needed to enable batch operation from file
        private readonly Queue<Action> _commandQueue;

        // Factory to generate concrete command from given name
        private readonly CommandFactory _commandFactory;

        // Factory to generate concrete command handler from given name
        private readonly CommandHandlerFactory _commandHandlerFactory;

        public ParkingLotCommandService(IScreenWriter screenWriter)
        {
            IScreenWriter writer = screenWriter ?? throw new ArgumentNullException();
            ICarSlotManager carSlotManager = new CarSlotManager(new Dictionary<int, Car>());
            _commandQueue = new Queue<Action>();
            _commandFactory = new CommandFactory();
            _commandHandlerFactory = new CommandHandlerFactory(carSlotManager, writer);
        }

        public void Register(string commandName, string[] args = null)
        {
            ICommand command = _commandFactory.Create(commandName, args);
            ICommandHandler handler = _commandHandlerFactory.Create(commandName);
            if (command == null || handler == null)
            {
                throw new CommandNotRecognizedException();
            }
            _commandQueue.Enqueue(() => handler.Execute(command));
        }

        public void RegisterAll(ICollection<KeyValuePair<string, string[]>> namesAndArgs)
        {
            foreach (KeyValuePair<string, string[]> namesAndArg in namesAndArgs)
            {
                Register(namesAndArg.Key, namesAndArg.Value);
            }
        }

        public IEnumerable<Action> GetRegisteredCommands()
        {
            return _commandQueue;
        }

        public void Execute()
        {
            Action command = _commandQueue.Dequeue();
            command.Invoke();
        }

        public void ExecuteAll()
        {
            while (_commandQueue.Any())
            {
                Action command = _commandQueue.Dequeue();
                command.Invoke();                
            }
        }
    }
}