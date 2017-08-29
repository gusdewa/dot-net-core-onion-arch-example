using System.Text;

namespace ParkingLot.Domain.Utils
{
    public interface IFileReader
    {
        StringBuilder ReadAsString(string fileName);
    }
}
