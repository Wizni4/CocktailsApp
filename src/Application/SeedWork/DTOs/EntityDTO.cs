/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.SeedWork
{
    /// <summary>
    /// Base Data Transfer Object (DTO) class representing an entity with an identifier.
    /// </summary>
    public class EntityDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public Guid Id { get; set; }
        public string? ImageId { get; set; }
    }
}
