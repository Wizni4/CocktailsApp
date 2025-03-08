/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using Application.SeedWork;


namespace Application.Cocktails
{
    public class CocktailService(IHandlerManager handler) : ICocktailService
    {
        private readonly IHandlerManager _handler = handler;
        public async Task<IEnumerable<CocktailDTO>> GetCocktailsAsync()
        {
            var query = new GetCocktailsQuery();
            return await _handler.Set<GetCocktailsQuery, List<CocktailDTO>>().Handle(query);
        }
    }
}
