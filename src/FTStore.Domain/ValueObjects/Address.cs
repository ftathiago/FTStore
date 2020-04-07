using FTStore.Lib.Common.ValueObjects;

namespace FTStore.Domain.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; protected set; }
        public string AddressNumber { get; protected set; }
        public string Neighborhood { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string ZIPCode { get; protected set; }
        public Address(
            string street,
            string addressNumber,
            string neighborhood,
            string city,
            string state,
            string zipCode)
        {
            Street = street;
            AddressNumber = addressNumber;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZIPCode = zipCode;
        }

        protected override bool EqualsCore(Address other)
        {
            return Street == other.Street &&
                AddressNumber == other.AddressNumber &&
                Neighborhood == other.Neighborhood &&
                City == other.City &&
                State == other.State &&
                ZIPCode == other.ZIPCode;
        }

        protected override int GetHashCodeCore()
        {
            return (GetType().GetHashCode() * 42) +
                Street.GetHashCode() +
                AddressNumber.GetHashCode() +
                Neighborhood.GetHashCode() +
                City.GetHashCode() +
                State.GetHashCode() +
                ZIPCode.GetHashCode();
        }
    }
}
