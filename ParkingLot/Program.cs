using System;
using ParkingLot.ApplicationService;
using ParkingLot.Infrastructure.Console;

namespace ParkingLot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleWriter writer = new ConsoleWriter();
            IParkingLotCommandService appService = new ParkingLotCommandService(writer);
            appService.Register("1111", new[] {"10"});
          
            appService.ExecuteAll();
            Console.ReadLine();
        }
    }
}