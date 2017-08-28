using System;
using System.Text;
using ParkingLot.Domain.Reports;

namespace ParkingLot.Domain.Cars
{
    /// <summary>
    /// Car entity identifiable having unique Registration Number and Color
    /// </summary>
    public class Car : IEquatable<Car>, IStatusReport
    {
        private readonly string _registrationNumber;
        private readonly string _color;

        public Car(string registrationNumber, string color)
        {
            _registrationNumber = registrationNumber ?? throw new ArgumentNullException();
            _color = color ?? throw new ArgumentNullException();
        }

        public string RegistrationNumber
        {
            get => _registrationNumber;
        }

        public string Color
        {
            get => _color;
        }

        public bool Equals(Car other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_registrationNumber, other._registrationNumber) && string.Equals(_color, other._color);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Car) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // 397 is a prime of sufficient size providing a better distribution of hash codes
                return ((_registrationNumber != null ? _registrationNumber.GetHashCode() : 0) * 397) ^ (_color != null ? _color.GetHashCode() : 0);
            }
        }

        public StringBuilder GenerateStatusReport() => new StringBuilder($"{_registrationNumber}\t{_color}");
    }
}
