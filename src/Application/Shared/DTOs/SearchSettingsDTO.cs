/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Stock;
using CocktailsApp.Domain.OrderAggregate;
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Shared
{
    public class SearchSettingsDTO
    {
        public int Limit { get; set; }
    }
}
