using ParkingLot.Domain.Cars.Specifications;

namespace ParkingLot.ApplicationService.Commands
{
    public class PrintStatusCommand : ICarQueryCommand
    {
        public PrintStatusCommand()
        {
            Name = "status";
            // No specification needed
            QuerySpecification = null;
        }

        public string Name { get; }
        public ICarSpecification QuerySpecification { get; }
    }
}