/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.Shared;

using MediatR;


namespace CocktailsApp.Application.Cocktail
{
    public class CocktailService(IMediator mediator) : ICocktailService
    {
        private readonly IMediator _mediator = mediator;

        public Task<CocktailDTO> CreateCocktailAsync(string name, IEnumerable<CocktailIngredientDTO> ingredients)
        {
            var command = new CreateCocktailCommand(name, [.. ingredients]);
            return _mediator.Send(command);
        }

        public Task<IEnumerable<CocktailDTO>> GetAllCocktailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CocktailDTO> GetCocktailByIdAsync(Guid cocktailId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CocktailDTO>> GetCocktailsByIngredientsAsync(List<IngredientDTO> ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CocktailDTO>> GetCocktailsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
