using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public sealed record RolePermissionsUpdateModel(
        Guid Id,
        IEnumerable<PermissionType> Permissions
    );
}
