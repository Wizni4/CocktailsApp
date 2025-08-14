using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record CreateClubRequest(
        AddressRequest Address,
        string Description,
        string Name,
        string Visibility
    ) : IRequest;


}
