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
using CocktailsApp.Domain.Shared;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;

namespace CocktailsApp.Application.Cocktail
{
    public class CreateCocktailCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : 
        ICommandHandler<CreateCocktailCommand, CocktailDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        public async Task<CocktailDTO> Handle(CreateCocktailCommand request, CancellationToken cancellationToken)
        {
            var cocktailBuilder = new CocktailBuilder()
                .WithName(request.Descripotion)
                .WithName(request.Name);

            foreach (var ingredient in request.Ingredients)
                cocktailBuilder.AddIngredient(_autoMapper.Map<Ingredient>(ingredient), 1);

            var cocktail = cocktailBuilder.Build();
            _unitOfWork.Set<DomainCocktail>().Create(cocktail);
            await _unitOfWork.SaveChangesAsync();
            return _autoMapper.Map<CocktailDTO>(cocktail);
        }
    }
}
