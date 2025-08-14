using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Ingredients
{
    public sealed class DeleteIngredientCommandHandler(
        ICurrentUser user,
        IIngredientRepository ingredientRepository
    ) : ICommandHandler<DeleteIngredientCommand, Unit>
    {
        private readonly ICurrentUser _user = user;
        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

        public async Task<Unit> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.ReadAsync(
                new LoadIngredientCommandSpecification(request.Id),
                cancellationToken);

            // Mark ingredient as deleted
            ingredient!.DeleteIngredient(_user.UserId);

            await _ingredientRepository.DeleteAsync(ingredient!, cancellationToken);
            return Unit.Value;
        }
    }
}
