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


namespace CocktailsApp.Application.Search
{
    public class SearchResultDTO : EntityDTO
    {
        public required string Name { get; set; }
        public required Type Type { get; set; }
        public required int Relevance { get; set; }
    }
}
