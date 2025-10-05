using HotelBooking.Domain.Exceptions;
using SharedKernel.Common;

namespace HotelBooking.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string Building { get; private set; }
        public string? ZipCode { get; private set; } // поштовий індекс

        private Address() { }

        public Address(string country, string city, string street, string building, string? zipCode)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new InvalidAddressDataException("Country cannot be empty");
            if (string.IsNullOrWhiteSpace(city))
                throw new InvalidAddressDataException("City cannot be empty");
            if (string.IsNullOrWhiteSpace(street))
                throw new InvalidAddressDataException("Street cannot be empty");
            if (string.IsNullOrWhiteSpace(building))
                throw new InvalidAddressDataException("Building cannot be empty");

            Country = country;
            City = city;
            Street = street;
            Building = building;
            ZipCode = zipCode;
        }


        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Street;
            yield return Building;
            yield return ZipCode;
        }

        public override string ToString() => $"{Country}, {City}, {Street}, {Building}, {ZipCode}";

        public static implicit operator string(Address address) => address.ToString();
    }
}
