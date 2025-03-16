/*
 * Domain namespaces
 */

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    /// <summary>
    /// Enumerates the permissions that can be assigned to roles or members within a club.
    /// </summary>
    public enum ClubPermission
    {
        /// <summary>
        /// Permission to add a <see cref="ClubCocktail"/> to the <see cref="Club"/>.
        /// </summary>
        AddCocktail,

        /// <summary>
        /// Permission to add a <see cref="ClubMember"/> to the <see cref="Club"/>.
        /// </summary>
        AddMember,

        /// <summary>
        /// Permission to add a <see cref="ClubPermission"/> to a <see cref="ClubMember"/>.
        /// </summary>
        AddPermissionToMember,

        /// <summary>
        /// Permission to add a <see cref="ClubPermission"/> to a <see cref="ClubRole"/>.
        /// </summary>
        AddRolePermission,

        /// <summary>
        /// Permission to assign a <see cref="ClubRole"/> to a <see cref="ClubMember"/>.
        /// </summary>
        AddRoleToMember,

        /// <summary>
        /// Permission to create a new <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        CreateRole,

        /// <summary>
        /// Permission to delete a <see cref="ClubRole"/> from the <see cref="Club"/>.
        /// </summary>
        DeleteRole,

        /// <summary>
        /// Permission to change the <see cref="Club"/>'s address.
        /// </summary>
        ChangeAddress,

        /// <summary>
        /// Permission to change the <see cref="Club"/>'s description.
        /// </summary>
        ChangeDescription,

        /// <summary>
        /// Permission to change the <see cref="Club"/>'s name.
        /// </summary>
        ChangeName,

        /// <summary>
        /// Permission to change the name of a <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        ChangeRoleName,

        /// <summary>
        /// Permission to change the <see cref="Club"/>'s visibility settings.
        /// </summary>
        ChangeVisibility,

        /// <summary>
        /// Permission to remove a <see cref="ClubCocktail"/> from the <see cref="Club"/>.
        /// </summary>
        RemoveCocktail,

        /// <summary>
        /// Permission to remove a <see cref="ClubMember"/> from the <see cref="Club"/>.
        /// </summary>
        RemoveMember,

        /// <summary>
        /// Permission to remove a <see cref="ClubPermission"/> from a <see cref="ClubMember"/>.
        /// </summary>
        RemovePermissionToMember,

        /// <summary>
        /// Permission to remove a <see cref="ClubPermission"/> from a <see cref="ClubRole"/>.
        /// </summary>
        RemoveRolePermission,

        /// <summary>
        /// Permission to remove a <see cref="ClubRole"/> from a <see cref="ClubMember"/>.
        /// </summary>
        RemoveRoleToMember,
    }
}
