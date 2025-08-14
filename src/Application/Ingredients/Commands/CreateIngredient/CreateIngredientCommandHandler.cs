using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Ingredients;


namespace CocktailsApp.Application.Ingredients
{
    public sealed class CreateIngredientCommandHandler(
        ICurrentUserService user,
        IIngredientRepository ingredientRepository
    ) : ICommandHandler<CreateIngredientCommand, Guid>
    {
        private readonly ICurrentUserService _user = user;
        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;
        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientBuilder = new IngredientFactory()
                .WithCreatorId(_user.UserId)
                .WithName(request.Name)
                .WithType(request.Type);

            if (request.IsAlcoholic != null && request.IsAlcoholic == true)
                ingredientBuilder.AsAlcoholic();

            if (request.Allergens is not null)
                foreach (var allergen in request.Allergens)
                    ingredientBuilder.AddAllergen(allergen);

            var ingredient = ingredientBuilder.Build();

            await _ingredientRepository.CreateAsync(ingredient, cancellationToken);
            return ingredient.Id;
        }
    }
}
