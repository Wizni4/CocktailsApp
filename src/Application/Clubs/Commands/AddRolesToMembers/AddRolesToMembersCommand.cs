using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

using MediatR;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Command to grant roles to a club member, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the permission change is being made.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public sealed record AddRolesToMembersCommand(
        Guid ClubId,
        IEnumerable<MemberRolesUpdateModel> Members
    ) : ClubCommand<Unit>(ClubId), IIdempotentCommand;
}
