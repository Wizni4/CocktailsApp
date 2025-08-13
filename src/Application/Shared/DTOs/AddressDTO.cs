/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Shared
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a physical address.
    /// Contains all standard address components such as street, city, postal code, and country.
    /// </summary>
    public sealed class AddressDTO : EntityDTO
    {
        public string Street { get; set; } = default!;
        public string StreetNumber { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
