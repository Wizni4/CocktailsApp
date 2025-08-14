using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Cocktails
{
    public class DeleteCocktailCommandHandler(
        ICurrentUserService user,
        ICocktailRepository cocktailRepository
    ) : ICommandHandler<DeleteCocktailCommand, Unit>
    {
        private readonly ICurrentUserService _user = user;
        private readonly ICocktailRepository _cocktailRepository = cocktailRepository;

        public async Task<Unit> Handle(DeleteCocktailCommand request, CancellationToken cancellationToken)
        {
            var cocktail = await _cocktailRepository.ReadAsync(
                new LoadCocktailCommandSpecification(request.Id),
                cancellationToken);

            // Mark cocktail as deleted
            cocktail!.DeleteCocktail(_user.UserId);

            await _cocktailRepository.DeleteAsync(cocktail!, cancellationToken);
            return Unit.Value;
        }
    }
}
