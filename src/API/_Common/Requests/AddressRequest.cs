
namespace CocktailsApp.API.Common
{
    public sealed record AddressRequest(
        string Street,
        string StreetNumber,
        string City,
        string PostalCode,
        string State,
        string Country
    );
}
