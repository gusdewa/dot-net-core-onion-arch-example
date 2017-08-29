using ParkingLot.Domain.Cars.Specifications;

namespace ParkingLot.ApplicationService.Commands
{
    public class GetSlotNumbersByColorCommand : ICarQueryCommand
    {
        public GetSlotNumbersByColorCommand(string color)
        {
            Name = "slot_numbers_for_cars_with_colour";
            QuerySpecification = new CarHavingColor(color);
        }

        public string Name { get; }
        public ICarSpecification QuerySpecification { get; }
    }
}