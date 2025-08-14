using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Defines a repository interface for accessing and managing <see cref="Club"/> entities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface extends the <see cref="IRepository{T}"/> interface.
    /// </para>
    /// <para>
    /// It serves as a contract for implementing data access logic related to clubs.
    /// </para>
    /// </remarks>
    public interface IClubRepository : IRepository<Club>;
}
