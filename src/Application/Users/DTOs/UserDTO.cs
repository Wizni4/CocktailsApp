/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Users
{
    public sealed class UserDTO : EntityDTO, IImageDTO
    {
        public Guid Id { get; set; } = default;
        public string Username { get; set; } = default!;
        public string? ImageId { get; set; } = null;
    }
}
