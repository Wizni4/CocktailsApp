/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Command to create a new club.
    /// </summary>
    /// <param name="address">The address of the club, provided as an <see cref="AddressDTO"/>.</param>
    /// <param name="description">A textual description of the club.</param>
    /// <param name="name">The name of the club.</param>
    /// <param name="ownerId">The identifier of the user who will be the owner of the club.</param>
    /// <param name="visibility">The visibility setting of the club (e.g., public or private).</param>
    public class CreateClubCommand(AddressDTO address, string description, string name, Guid ownerId, ClubVisibility visibility) : ICommand<ClubDTO>
    {
        /// <summary>
        /// Gets the address of the club.
        /// </summary>
        public AddressDTO Address { get; } = address;

        /// <summary>
        /// Gets the description of the club.
        /// </summary>
        public string Description { get; } = description;

        /// <summary>
        /// Gets the name of the club.
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// Gets the ID of the user who will own the club.
        /// </summary>
        public Guid OwnerId { get; } = ownerId;

        /// <summary>
        /// Gets the visibility setting of the club.
        /// </summary>
        public ClubVisibility Visibility { get; } = visibility;
    }

}
