/*
 * Domain namespaces
 */

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public enum ClubAction
    {
        AddCocktail,
        AddMember,
        AddPermissionToMember,
        AddRolePermission,
        AddRoleToMember,
        CreateRole,
        ChangeAddress,
        ChangeDescription,
        ChangeName,
        RemoveCocktail,
        RemoveMember,
    }
}
