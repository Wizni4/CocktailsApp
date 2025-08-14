
namespace CocktailsApp.Application.Common
{
    public interface IIdempotencyKeyService
    {
        string? Value { get; }
    }
}
