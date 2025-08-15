
using CocktailsApp.Domain.Common;

using Newtonsoft.Json;



namespace CocktailsApp.Domain.Clubs
{
    /// <summary>
    /// Represents an immutable address value object composed of standard address components.
    /// Inherits from <see cref="ValueObject"/> to support equality based on property values rather than reference.
    /// </summary>
    public sealed class Address : ValueObject
    {
        /// <summary>
        /// Gets the name of the street (e.g., "Main Street").
        /// </summary>
        public string Street { get => _street; }
        private readonly string _street = null!;

        /// <summary>
        /// Gets the number of the street (e.g., "42B").
        /// </summary>
        public string StreetNumber { get => _streetNumber; }
        private readonly string _streetNumber = null!;

        /// <summary>
        /// Gets the name of the city (e.g., "Paris").
        /// </summary>
        public string City { get => _city; }
        private readonly string _city = null!;

        /// <summary>
        /// Gets the postal or ZIP code (e.g., "75001").
        /// </summary>
        public string PostalCode { get => _postalCode; }
        private readonly string _postalCode = null!;

        /// <summary>
        /// Gets the state or province (e.g., "Île-de-France").
        /// </summary>
        public string State { get => _state; }
        private readonly string _state = null!;

        /// <summary>
        /// Gets the country name (e.g., "France").
        /// </summary>
        public string Country { get => _country; }
        private readonly string _country = null!;

        private Address() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class with the specified components.
        /// This constructor is internal to enforce controlled creation via factory methods or entities.
        /// </summary>
        /// <param name="street">The street name.</param>
        /// <param name="streetNumber">The street number.</param>
        /// <param name="city">The city name.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="state">The state or province.</param>
        /// <param name="country">The country name.</param>
        [JsonConstructor]
        internal Address(string street, string streetNumber, string city, string postalCode, string state, string country)
        {
            _street = street;
            _streetNumber = streetNumber;
            _city = city;
            _postalCode = postalCode;
            _state = state;
            _country = country;
        }
    }
}
