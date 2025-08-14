using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Cocktails;


namespace CocktailsApp.Application.Cocktails
{
    public class CreateCocktailCommandHandler(
        ICurrentUserService user,
        ICocktailRepository cocktailRepository
    ) : ICommandHandler<CreateCocktailCommand, Guid>
    {
        private readonly ICocktailRepository _cocktailRepository = cocktailRepository;
        private readonly ICurrentUserService _user = user;

        public async Task<Guid> Handle(CreateCocktailCommand request, CancellationToken cancellationToken)
        {
            var cocktailBuilder = new CocktailFactory()
                .WithCreatorId(_user.UserId)
                .WithDescription(request.Description)
                .WithName(request.Name);

            foreach (var ingredient in request.Ingredients)
                cocktailBuilder.AddIngredient(
                    ingredient.Id,
                    ingredient.Quantity,
                    ingredient.Unit
                );

            var cocktail = cocktailBuilder.Build();
            await _cocktailRepository.CreateAsync(cocktail, cancellationToken);
            return cocktail.Id;
        }
    }
}
