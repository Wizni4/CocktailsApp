namespace CocktailsApp.API.Auth
{
    public interface ICookieService
    {
        string GetRefreshTokenFromCookie();
        void SetRefreshTokenCookie(string? refreshToken);
        void DeleteRefreshTokenCookie();
    }
}
