using FTStore.Domain.Common.ValueObjects;

namespace FTStore.Domain.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; private set; }
        public int AddressNumber { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Address(string street, int addressNumber, string neighborhood, string city, string state)
        {
            Street = street;
            AddressNumber = addressNumber;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        protected override bool EqualsCore(Address other)
        {
            return Street.Equals(other.Street) &&
                AddressNumber.Equals(other.AddressNumber) &&
                Neighborhood.Equals(other.Neighborhood) &&
                City.Equals(other.City) &&
                State.Equals(other.State);
        }

        protected override int GetHashCodeCore()
        {
            return (GetType().GetHashCode() * 42) +
                Street.GetHashCode() +
                AddressNumber.GetHashCode() +
                Neighborhood.GetHashCode() +
                City.GetHashCode() +
                State.GetHashCode();
        }
    }
}
