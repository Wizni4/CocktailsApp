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

namespace CocktailsApp.Application.Cocktail
{
    public class CreateCocktailCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : 
        ICommandHandler<CreateCocktailCommand, CocktailDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        public async Task<CocktailDTO> Handle(CreateCocktailCommand request, CancellationToken cancellationToken)
        {
            var ingredients = request.Ingredients.Select(i => _autoMapper.Map<CocktailIngredient>(i)).ToList();
            var cocktail = new CocktailBuilder()
                .WithName(request.Name)
                .AddIngredients(ingredients)
                .Build();
            _unitOfWork.Set<CocktailsApp.Domain.CocktailAggregate.Cocktail>().Create(cocktail);
            await _unitOfWork.SaveChangesAsync();

            return _autoMapper.Map<CocktailDTO>(cocktail);
        }
    }
}
