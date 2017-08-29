namespace ParkingLot.ApplicationService
{
    /// <summary>
    ///     Yes, you read it right. Command-command vs Query-command.
    ///     Command is to mutate the state.
    ///     Query is to retrieve the current state.
    ///     (Last) Command is an artifact of the command pattern.
    /// </summary>
    public interface ICarCommandCommand : ICommand
    {
        string FeedbackMessage { get; }
    }
}