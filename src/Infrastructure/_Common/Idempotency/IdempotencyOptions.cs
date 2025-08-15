
namespace CocktailsApp.Infrastructure.Common
{
    public sealed class IdempotencyOptions
    {
        public string? Namespace { get; init; }
        public int? LockTtlSeconds { get; init; }
        public int? ResultTtlSeconds { get; init; }
    }
}
