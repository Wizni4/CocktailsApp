
namespace CocktailsApp.Application.Cocktails
{
    public interface ICocktailQueries
    {
        Task<CocktailDetails?> GetCocktailDetailsAsync(Guid cocktailId, CancellationToken cancellationToken);
        Task<CocktailListItem?> GetCocktailListItemAsync(Guid cocktailId, CancellationToken cancellationToken);
    }
}
