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


namespace CocktailsApp.Application.User
{
    public interface IUserService : IService<UserDTO>
    {
        Task<UserDTO> CreateUserAsync();
    }
}
