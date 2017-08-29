using System;
using System.Collections.Generic;

namespace ParkingLot.ApplicationService
{
    public interface IParkingLotCommandService
    {
        void Execute();
        void ExecuteAll();
        IEnumerable<Action> GetRegisteredCommands();
        void Register(string commandName, string[] args = null);
        void RegisterAll(IEnumerable<KeyValuePair<string, string[]>> namesAndArgs);
        IEnumerable<KeyValuePair<string, string[]>> ExtractCommandStatements(string longString);
    }
}