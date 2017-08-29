namespace ParkingLot.ApplicationService
{
    public interface ICommandHandler
    {
        void Execute(ICommand command);
    }
}