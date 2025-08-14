namespace CocktailsApp.Application.Common
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a physical address.
    /// Contains all standard address components such as street, city, postal code, and country.
    /// </summary>
    public sealed record Address(
        string Street,
        string StreetNumber,
        string City,
        string PostalCode,
        string State,
        string Country 
    ) : Entity;
}
