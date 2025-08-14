using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

using MediatR;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Command to delete roles, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the role is being deleted.</param>
    /// <param name="RoleIds">The list of unique identifiers of roles to delete.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public sealed record DeleteRolesCommand(
        Guid ClubId,
        IEnumerable<Guid> RoleIds,
        Guid RequestId
    ) : ClubCommand<Unit>(ClubId), IIdempotentCommand
    {
        public string IdempotencyKey => $"DeleteRoles:{RequestId}";
    }
}
