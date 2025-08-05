/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Defines a repository interface for accessing and managing <see cref="DomainClub"/> entities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface extends the <see cref="IRepository{T}"/> interface.
    /// </para>
    /// <para>
    /// It serves as a contract for implementing data access logic related to clubs.
    /// </para>
    /// </remarks>
    public interface IClubRepository : IRepository<DomainClub>
    {
        Task<DomainClub?> GetClubBydIdAsync(Guid clubId, Func<IIncludable<DomainClub>, IIncludable>? includes = null);
    }
}
