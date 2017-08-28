using System.Text;

namespace ParkingLot.Domain.Reports
{
    public interface IStatusReport
    {
        StringBuilder GenerateStatusReport();
    }
}
