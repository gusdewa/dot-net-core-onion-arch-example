using System;
using System.Collections.Generic;
using System.Text;
using ParkingLot.ApplicationService;
using ParkingLot.Infrastructure.Console;
using ParkingLot.Infrastructure.File;

namespace ParkingLot.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string input;
            do
            {
                do
                {
                    input = Console.ReadLine();
                } while (!input.IsStartProgramStatements() && !input.IsBreakProgramStatement());

                // Break the program immediately
                if (input.IsBreakProgramStatement()) return;

                // Instantiate app service and dependencies
                ConsoleWriter consoleWriter = new ConsoleWriter();
                IParkingLotCommandService service = new ParkingLotCommandService(consoleWriter);

                if (input.ReadFromFileStatement())
                {
                    TextFileReader textFileReader = new TextFileReader(consoleWriter);
                    StringBuilder content = textFileReader.ReadAsString(input.GetFileName());

                    // Extract each line into list of command statements
                    IEnumerable<KeyValuePair<string, string[]>> commandStatements =
                        service.ExtractCommandStatements(content.ToString());

                    // Register the list of commands at once
                    service.RegisterAll(commandStatements);

                    // Exceute based on the order of the queue
                    service.ExecuteAll();
                }
                else
                {
                    do
                    {
                        input = Console.ReadLine();
                        if (input.IsBreakProgramStatement()) return;

                        // Register the command
                        service.Register(input.GetCommandName(), input.GetArguments());
                        // Execute the command. Command queue is always 0 or 1.
                        service.Execute();
                    } while (!input.IsBreakProgramStatement());
                }
            } while (!input.IsBreakProgramStatement());
        }
    }
}