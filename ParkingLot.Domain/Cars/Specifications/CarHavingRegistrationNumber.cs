namespace ParkingLot.Domain.Cars.Specifications
{
    public class CarHavingRegistrationNumber : ICarSpecification
    {
        private readonly string _registrationNumber;

        public CarHavingRegistrationNumber(string registrationNumber)
        {
            _registrationNumber = registrationNumber;
        }

        public bool IsSatisfiedBy(Car car) => car.RegistrationNumber == _registrationNumber;
    }
}
