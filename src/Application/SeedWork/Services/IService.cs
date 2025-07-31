/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using CocktailsApp.Application.Cocktail;
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.SeedWork
{
    /// <summary>
    /// Marker interface for application services returning DTOs.
    /// </summary>
    /// <typeparam name="TDTO">The type of Data Transfer Object (DTO) used by the service.</typeparam>
    public interface IService<TDTO> where TDTO : EntityDTO
    {
    }
}
