using System.Collections.Generic;
using System.Linq;

namespace ParkingLot.ConsoleApp
{
    public static class ConsoleHelper
    {
        public static bool IsStartProgramStatements(this string input)
        {
            return input.Contains("parking_lot");
        }

        public static bool IsBreakProgramStatement(this string input)
        {
            string[] words = {"exit", "quit"};
            return input == null || words.Any(input.Contains);
        }

        public static bool ReadFromFileStatement(this string input)
        {
            return input.Contains(' ') && input.Contains(".txt");
        }

        public static string GetFileName(this string input)
        {
            return input.Split(' ')[1];
        }

        public static string GetCommandName(this string input)
        {
            return input.Split(' ')[0];
        }

        public static string[] GetArguments(this string input)
        {
            Queue<string> words = new Queue<string>(input.Split(' '));
            words.Dequeue();
            return words.ToArray();
        }
    }
}
