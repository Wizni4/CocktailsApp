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
    public abstract class Service<TDTO>() : IService<TDTO> where TDTO : EntityDTO
    {
    }
}
