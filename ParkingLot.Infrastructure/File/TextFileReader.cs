using System;
using System.IO;
using System.Text;
using ParkingLot.Domain.Utils;

namespace ParkingLot.Infrastructure.File
{
    public class TextFileReader : IFileReader
    {
        private readonly IScreenWriter _screenWriter;

        public TextFileReader(IScreenWriter screenWriter)
        {
            _screenWriter = screenWriter;
        }

        public StringBuilder ReadAsString(string fileName)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Open);
            }
            catch (Exception e)
            {
                _screenWriter.WriteLine(e.Message);
            }

            StringBuilder content = new StringBuilder();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line;
                do
                {
                    line = reader.ReadLine();
                    content.AppendLine(line);
                } while (!string.IsNullOrEmpty(line));
            }
            return content;
        }
    }
}
