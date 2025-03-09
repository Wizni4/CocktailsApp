/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Cocktails
{
    public class CreateCocktailCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : ICommandHandler<CreateCocktailCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task Handle(CreateCocktailCommand command)
        {
            var ingredients = command.Ingredients.Select(ingredient => _autoMapper.Map<CocktailIngredient>(ingredient)).ToList();
            var cocktail = new CocktailBuilder()
                .AddName(command.Name)
                .AddIngredients(ingredients)
                .Build();
            _unitOfWork.Set<Cocktail>().Create(cocktail);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
