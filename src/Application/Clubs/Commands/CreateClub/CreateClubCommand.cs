using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Command to create a new club.
    /// </summary>
    /// <param name="address">The address of the club, provided as an <see cref="Common.Address"/>.</param>
    /// <param name="description">A textual description of the club.</param>
    /// <param name="name">The name of the club.</param>
    /// <param name="ownerId">The identifier of the user who will be the owner of the club.</param>
    /// <param name="visibility">The visibility setting of the club (e.g., public or private).</param>
    public sealed record CreateClubCommand(
        Common.Address Address,
        string Description,
        string Name,
        Visibility? Visibility
    ) : ICommand<Guid>, IIdempotentCommand;
}
