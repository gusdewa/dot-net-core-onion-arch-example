namespace ParkingLot.ApplicationService.Commands
{
    public class CreateParkingLotCommand : ICarCommandCommand
    {
        public CreateParkingLotCommand(int maxCapacity)
        {
            Name = "create_parking_lot";
            MaxCapacity = maxCapacity;
            FeedbackMessage = $"Created a parking lot with {maxCapacity} slots";
        }

        public int MaxCapacity { get; }
        public string Name { get; }
        public string FeedbackMessage { get; }
    }
}