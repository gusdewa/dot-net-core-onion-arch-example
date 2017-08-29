namespace ParkingLot.ApplicationService.Commands
{
    public class LeaveCarCommand : ICarCommandCommand
    {
        public LeaveCarCommand(int slotNumber)
        {
            Name = "leave";
            SlotNumber = slotNumber;
            FeedbackMessage = $"Slot number {slotNumber} is free";
        }

        public int SlotNumber { get; }

        public string Name { get; }
        public string FeedbackMessage { get; }
    }
}