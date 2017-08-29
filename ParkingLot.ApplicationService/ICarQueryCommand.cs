using ParkingLot.Domain.Cars.Specifications;

namespace ParkingLot.ApplicationService
{
    /// <summary>
    ///     Command-command is to mutate the state.
    ///     Query-command is to retrieve the current state.
    ///     (Last) Command is an artifact of the command pattern.
    /// </summary>
    public interface ICarQueryCommand : ICommand
    {
        ICarSpecification QuerySpecification { get; }
    }
}