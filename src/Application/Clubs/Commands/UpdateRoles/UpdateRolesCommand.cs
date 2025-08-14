using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

using MediatR;

namespace CocktailsApp.Application.Clubs
{
    public sealed record UpdateRolesCommand(
        Guid ClubId,
        IEnumerable<UpdateRoleModel> Roles,
        Guid RequestId
    ) : ClubCommand<Unit>(ClubId), IIdempotentCommand
    {
        public string IdempotencyKey => $"UpdateRoles:{RequestId}";
    }

    public sealed record UpdateRoleModel(
        Guid Id,
        string? Name,
        IEnumerable<PermissionType>? Permissions
    );
}
