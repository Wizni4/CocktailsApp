
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetIngredientDetailsQueryHandler(
        IIngredientQueries queries
    ) : IQueryHandler<GetIngredientDetailsQuery, IngredientDetails?>
    {
        private readonly IIngredientQueries _queries = queries;

        public Task<IngredientDetails?> Handle(GetIngredientDetailsQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetIngredientDetailsAsync(
                request.IngredientId,
                cancellationToken);
        }
    }
}
