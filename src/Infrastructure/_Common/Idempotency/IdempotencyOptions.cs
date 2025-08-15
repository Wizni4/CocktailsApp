
namespace CocktailsApp.Infrastructure.Common
{
    public sealed class IdempotencyOptions
    {
        public string Namespace { get; init; } = "cocktailsapp:idem:";
        public int LockTtlSeconds { get; init; } = 30;
        public int ResultTtlSeconds { get; init; } = 48 * 3600;
    }
}
