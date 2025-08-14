
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class GetCocktailListItemQueryHandler(
        ICocktailQueries queries
    ) : IQueryHandler<GetCocktailListItemQuery, CocktailListItem?>
    {
        private readonly ICocktailQueries _queries = queries;
        public Task<CocktailListItem?> Handle(GetCocktailListItemQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetCocktailListItemAsync(
                request.CocktailId,
                cancellationToken);
        }
    }
}
