/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Command to add a list of cocktail to a club, initiated by a user or club member.
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club to which the cocktail will be added.</param>
    /// <param name="CocktailIds">The list of unique identifier of the cocktails to be added.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record AddCocktailsCommand(
        Guid ClubId,
        IEnumerable<Guid> CocktailIds,
        Guid ActorId
    ) : ICommand<IEnumerable<ClubCocktailDTO>>;
}
