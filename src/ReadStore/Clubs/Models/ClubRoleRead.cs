

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRoleRead
    {
        public Guid ClubId { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsOwnerRole { get; set; }
    }

    public sealed class ClubRolePermissionRead
    {
        public Guid ClubId { get; set; }
        public Guid RoleId { get; set; }
        public string Permission { get; set; } = default!;
    }
}
