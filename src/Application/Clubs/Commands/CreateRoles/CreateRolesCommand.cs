using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public sealed record CreateRolesCommand(
        Guid ClubId,
        IEnumerable<CreateRoleModel> NewRoles
    ) : ClubCommand<IEnumerable<Guid>>(ClubId), IIdempotentCommand;

    public sealed record CreateRoleModel(
        string Name,
        IEnumerable<PermissionType>? Permissions
    );
}
