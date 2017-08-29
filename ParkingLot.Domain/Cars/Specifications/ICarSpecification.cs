namespace ParkingLot.Domain.Cars.Specifications
{
    public interface ICarSpecification
    {
        bool IsSatisfiedBy(Car car);
    }
}