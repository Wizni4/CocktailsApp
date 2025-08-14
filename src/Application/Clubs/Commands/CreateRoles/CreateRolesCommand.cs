using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public sealed record CreateRolesCommand(
        Guid ClubId,
        IEnumerable<CreateRoleModel> NewRoles,
        Guid RequestId
    ) : ClubCommand<IEnumerable<Guid>>(ClubId), IIdempotentCommand
    {
        public string IdempotencyKey => $"CreateRoles:{RequestId}";
    }

    public sealed record CreateRoleModel(
        string Name,
        IEnumerable<PermissionType>? Permissions
    );
}
