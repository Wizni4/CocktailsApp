

namespace CocktailsApp.ReadStore.Ingredients
{
    public sealed class IngredientRead
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; } = default!;
        public string IngredientType { get; set; } = default!;
        public string? ImageId { get; set; }
        public bool IsAlcoholic { get; set; }
    }
}
