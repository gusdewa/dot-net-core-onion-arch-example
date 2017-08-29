using System.Text;

namespace ParkingLot.Domain.Reports
{
    public interface IStatusReportPrintable
    {
        StringBuilder GenerateStatusReport();
    }
}
