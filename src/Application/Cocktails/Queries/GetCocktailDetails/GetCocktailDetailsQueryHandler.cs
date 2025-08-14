using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class GetCocktailDetailsQueryHandler(
        ICocktailQueries queries
    ) : IQueryHandler<GetCocktailDetailsQuery, CocktailDetails?>
    {
        private readonly ICocktailQueries _queries = queries;
        public Task<CocktailDetails?> Handle(GetCocktailDetailsQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetCocktailDetailsAsync(
                request.CocktailId,
                cancellationToken);
        }
    }
}
