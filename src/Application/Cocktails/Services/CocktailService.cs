/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.SeedWork;
using Application.Shared;

/*
 * Framework namespaces
 */
using AutoMapper;

using Domain.Shared;


namespace Application.Cocktails
{
    public class CocktailService(IHandlerManager handler, IMapper autoMapper) : ICocktailService
    {
        private readonly IHandlerManager _handler = handler;
        private readonly IMapper _autoMapper = autoMapper;

        public async Task CreateCocktailAsync(string name, IEnumerable<CocktailIngredientDTO> ingredients)
        {
            var command = new CreateCocktailCommand(name, [.. ingredients]);
            await _handler.Set<CreateCocktailCommand>().Handle(command);
        }

        public async Task<IEnumerable<CocktailDTO>> GetAllCocktailsAsync()
        {
            var query = new GetAllCocktailsQuery();
            return await _handler.Set<GetAllCocktailsQuery, List<CocktailDTO>>().Handle(query);
        }

        public async Task<CocktailDTO> GetCocktailByIdAsync(Guid cocktailId)
        {
            var query = new GetCocktailByIdQuery(cocktailId);
            return await _handler.Set<GetCocktailByIdQuery, CocktailDTO>().Handle(query);
        }

        public async Task<IEnumerable<CocktailDTO>> GetCocktailsByIngredientsAsync(List<IngredientDTO> ingredients)
        {
            var query = new GetCocktailsByIngredientsQuery([.. _autoMapper.Map<IEnumerable<Ingredient>>(ingredients)]);
            return await _handler.Set<GetCocktailsByIngredientsQuery, List<CocktailDTO>>().Handle(query);
        }

        public async Task<IEnumerable<CocktailDTO>> GetCocktailsByNameAsync(string name)
        {
            var query = new GetCocktailsByNameQuery(name);
            return await _handler.Set<GetCocktailsByNameQuery, List<CocktailDTO>>().Handle(query);
        }
    }
}
