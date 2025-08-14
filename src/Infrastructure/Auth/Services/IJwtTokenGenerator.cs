namespace CocktailsApp.Infrastructure.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId);
    }
}
