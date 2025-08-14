
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Ingredients;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetIngredientTypesQueryHandler
        : IQueryHandler<GetIngredientTypesQuery, IReadOnlyCollection<string>>
    {
        public Task<IReadOnlyCollection<string>> Handle(GetIngredientTypesQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<string> types = Enum
                .GetNames(typeof(IngredientType))
                .ToList()
                .AsReadOnly();

            return Task.FromResult(types);
        }
    }
}
