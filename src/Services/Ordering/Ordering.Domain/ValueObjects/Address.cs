namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;

        //protected constructor needed for EF
        protected Address() { }

        private Address(string firstname, string lastName, string emailAdress, string addressLine, string country, string state, string zipCode)
        {
            FirstName = firstname;
            LastName = lastName;
            EmailAddress = emailAdress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Of(string firstname, string lastName, string emailAdress, string addressLine, string country, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAdress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

            return new Address(firstname, lastName, emailAdress, addressLine, country, state, zipCode);
        }
    }
}
