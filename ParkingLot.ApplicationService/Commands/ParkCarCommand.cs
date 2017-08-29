using System;

namespace ParkingLot.ApplicationService.Commands
{
    public class ParkCarCommand : ICarCommandCommand
    {
        public ParkCarCommand(string registrationNumber, string color)
        {
            RegistrationNumber = registrationNumber ?? throw new ArgumentNullException();
            Color = color ?? throw new ArgumentNullException();
            Name = "park";
            FeedbackMessage = "Allocated slot number: {0}";
        }

        public string RegistrationNumber { get; }

        public string Color { get; }

        public string Name { get; }
        public string FeedbackMessage { get; }
    }
}