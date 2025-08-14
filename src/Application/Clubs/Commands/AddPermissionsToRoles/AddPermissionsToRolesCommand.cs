using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

using MediatR;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Command to grant a permissions to a club role, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the permission change is being made.</param>
    /// <param name="Roles">The unique identifier of the club role to whom the permission will be granted.</param>
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public sealed record AddPermissionsToRolesCommand(
        Guid ClubId,
        IEnumerable<RolePermissionsUpdateModel> Roles
    ) : ClubCommand<Unit>(ClubId), IIdempotentCommand;
}
