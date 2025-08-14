namespace CocktailsApp.Application.Auth
{
    public interface ITokenService
    {
        string? AccessToken { get; }
        string? RefreshToken { get; }
    }
}
