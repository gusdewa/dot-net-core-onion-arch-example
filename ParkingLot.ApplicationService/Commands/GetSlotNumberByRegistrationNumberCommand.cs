using ParkingLot.Domain.Cars.Specifications;

namespace ParkingLot.ApplicationService.Commands
{
    public class GetSlotNumberByRegistrationNumberCommand : ICarQueryCommand
    {
        public GetSlotNumberByRegistrationNumberCommand(string registrationNumber)
        {
            Name = "slot_number_for_registration_number";
            QuerySpecification = new CarHavingRegistrationNumber(registrationNumber);
        }

        public string Name { get; }
        public ICarSpecification QuerySpecification { get; }
    }
}