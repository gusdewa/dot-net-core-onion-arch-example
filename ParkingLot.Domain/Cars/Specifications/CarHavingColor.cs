namespace ParkingLot.Domain.Cars.Specifications
{
    public class CarHavingColor : ICarSpecification
    {
        private readonly string _color;

        public CarHavingColor(string color)
        {
            _color = color;
        }

        public bool IsSatisfiedBy(Car car) => car.Color == _color;
    }
}
