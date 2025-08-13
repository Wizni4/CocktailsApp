/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public sealed class GlobalSearchResultDTO : EntityDTO, IImageDTO
    {
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string? ImageId { get; set; } = null;
        public int Relevance { get; set; } = default!;
    }
}
