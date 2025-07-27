/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Shared
{
    /// <summary>
    /// Represents an immutable address value object composed of standard address components.
    /// Inherits from <see cref="ValueObject"/> to support equality based on property values rather than reference.
    /// </summary>
    public class Address : ValueObject
    {
        /// <summary>
        /// Gets the name of the street (e.g., "Main Street").
        /// </summary>
        public string Street { get; }

        /// <summary>
        /// Gets the number of the street (e.g., "42B").
        /// </summary>
        public string StreetNumber { get; }

        /// <summary>
        /// Gets the name of the city (e.g., "Paris").
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Gets the postal or ZIP code (e.g., "75001").
        /// </summary>
        public string PostalCode { get; }

        /// <summary>
        /// Gets the state or province (e.g., "Île-de-France").
        /// </summary>
        public string State { get; }

        /// <summary>
        /// Gets the country name (e.g., "France").
        /// </summary>
        public string Country { get; }

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
        internal Address(string street, string streetNumber, string city, string postalCode, string state, string country)
        {
            Street = street;
            StreetNumber = streetNumber;
            City = city;
            PostalCode = postalCode;
            State = state;
            Country = country;
        }
    }
}
