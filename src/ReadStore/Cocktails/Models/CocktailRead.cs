

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class CocktailRead
    {
        public Guid CocktailId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageId { get; set; }
    }

    public sealed class CocktailIngredientRead
    {
        public Guid CocktailId { get; set; }
        public Guid IngredientId { get; set; }
        public Guid CocktailIngredientId { get; set; }
        public string IngredientName { get; set; } = default!;
        public string? IngredientType { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = default!;
        public bool IsAlcoholic { get; set; }
    }
}
