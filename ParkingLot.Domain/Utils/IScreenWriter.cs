namespace ParkingLot.Domain.Utils
{
    public interface IScreenWriter
    {
        void WriteLine(string format, params object[] args);
    }
}
