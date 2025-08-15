

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRead
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Visibility { get; set; } = default!;
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ImageId { get; set; }
    }
}
