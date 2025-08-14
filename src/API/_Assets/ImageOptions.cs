namespace CocktailsApp.API.Assets
{
    public sealed class ImageOptions
    {
        public string PublicBaseUrl { get; init; } = "";
        public string ClubPath { get; init; } = "clubs/{id}";
        public string CocktailPath { get; init; } = "cocktails/{id}";
        public string IngredientPath { get; init; } = "ingredients/{id}";
        public string UserPath { get; init; } = "users/{id}";
        public string? DefaultClubImage { get; init; }
        public string? DefaultCocktailImage { get; init; }
        public string? DefaultIngredientImage { get; init; }
        public string? DefaultUserImage { get; init; }
    }
}
