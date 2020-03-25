using FTStore.Domain.Common.ValueObjects;

namespace FTStore.Domain.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; protected set; }
        public int AddressNumber { get; protected set; }
        public string Neighborhood { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string ZIPCode { get; protected set; }
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
