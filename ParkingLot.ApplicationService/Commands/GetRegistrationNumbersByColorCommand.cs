using ParkingLot.Domain.Cars.Specifications;

namespace ParkingLot.ApplicationService.Commands
{
    public class GetRegistrationNumbersByColorCommand : ICarQueryCommand
    {
        public GetRegistrationNumbersByColorCommand(string color)
        {
            Name = "registration_numbers_for_cars_with_colour";
            QuerySpecification = new CarHavingColor(color);
        }

        public string Name { get; }
        public ICarSpecification QuerySpecification { get; }
    }
}