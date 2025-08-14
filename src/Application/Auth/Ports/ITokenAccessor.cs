namespace CocktailsApp.Application.Auth
{
    public interface ITokenAccessor
    {
        string? AccessToken { get; }
        string? RefreshToken { get; }
    }
}
