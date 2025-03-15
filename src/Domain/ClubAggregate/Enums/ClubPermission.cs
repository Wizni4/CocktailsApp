/*
 * Domain namespaces
 */

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public enum ClubPermission
    {
        AddCocktail,
        AddMember,
        AddPermissionToMember,
        AddRolePermission,
        AddRoleToMember,
        CreateRole,
        DeleteRole,
        ChangeAddress,
        ChangeDescription,
        ChangeName,
        ChangeRoleName,
        ChangeVisibility,
        RemoveCocktail,
        RemoveMember,
        RemovePermissionToMember,
        RemoveRolePermission,
        RemoveRoleToMember,
    }
}
