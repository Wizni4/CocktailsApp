
using CocktailsApp.Application.Cocktails;


namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class CocktailQueries : ICocktailQueries
    {
        public Task<CocktailDetails?> GetCocktailDetailsAsync(Guid cocktailId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CocktailListItem?> GetCocktailListItemAsync(Guid cocktailId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
