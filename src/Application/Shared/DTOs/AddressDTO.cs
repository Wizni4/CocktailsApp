/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Shared
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a physical address.
    /// Contains all standard address components such as street, city, postal code, and country.
    /// </summary>
    public class AddressDTO
    {
        /// <summary>
        /// Gets or sets the name of the street (e.g., "Main Street").
        /// </summary>
        public required string Street { get; set; }

        /// <summary>
        /// Gets or sets the street number (e.g., "42B").
        /// </summary>
        public required string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the city name (e.g., "Paris").
        /// </summary>
        public required string City { get; set; }

        /// <summary>
        /// Gets or sets the postal or ZIP code (e.g., "75001").
        /// </summary>
        public required string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the state, province, or region (e.g., "Île-de-France").
        /// </summary>
        public required string State { get; set; }

        /// <summary>
        /// Gets or sets the country name (e.g., "France").
        /// </summary>
        public required string Country { get; set; }
    }
}
