using ParkingLot.Domain.Utils;

namespace ParkingLot.Infrastructure.Console
{
    public class ConsoleWriter : IScreenWriter
    {
        public void WriteLine(string format, params object[] args)
        {
            System.Console.WriteLine(format, args);
        }
    }
}
