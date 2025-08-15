

namespace CocktailsApp.ReadStore.Users
{
    public sealed class UserSummaryRead
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = default!;
        public string? ImageId { get; set; }
    }
}
