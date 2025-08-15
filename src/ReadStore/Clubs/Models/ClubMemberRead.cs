

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubMemberRead
    {
        public Guid ClubId { get; set; }
        public Guid ClubMemberId { get; set; }
        public Guid UserId { get; set; }
    }

    public sealed class ClubMemberRoleRead
    {
        public Guid ClubId { get; set; }
        public Guid ClubMemberId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = default!;
        public bool IsOwnerRole { get; set; }
    }
}
