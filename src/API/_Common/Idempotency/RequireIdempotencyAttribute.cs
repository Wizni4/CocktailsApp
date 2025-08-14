
namespace CocktailsApp.API.Common
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class RequireIdempotencyAttribute : Attribute
    {
        public string HeaderName => IdempotencyKey.HeaderName;
        public string HttpItemKey => IdempotencyKey.HttpItemKey;
    }
}
