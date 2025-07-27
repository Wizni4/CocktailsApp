/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Command to add a cocktail to a club, initiated by a user or club member.
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club to which the cocktail will be added.</param>
    /// <param name="CocktailId">The unique identifier of the cocktail to be added.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record AddCocktailCommand(Guid ClubId, Guid CocktailId, Guid ActorId) : ICommand<ClubDTO>
    {
        /// <summary>
        /// Gets the unique identifier of the <see cref="Domain.ClubAggregate.Club"/> to which the <see cref="Domain.CocktailAggregate.Cocktail"/> will be added.
        /// </summary>
        public Guid ClubId { get; } = ClubId;

        /// <summary>
        /// Gets the unique identifier of the <see cref="Domain.CocktailAggregate.Cocktail"/> to be added.
        /// </summary>
        public Guid CocktailId { get; } = CocktailId;

        /// <summary>
        /// Gets the identifier of the actor performing the action.
        /// This typically refers to a <see cref="ClubMember"/>, but may also represent a <see cref="Domain.UserAggregate.User"/>.
        /// </summary>
        public Guid ActorId { get; } = ActorId;
    }

}
