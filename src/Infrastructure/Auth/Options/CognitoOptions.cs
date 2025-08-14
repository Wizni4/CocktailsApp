
namespace CocktailsApp.Infrastructure.Auth
{
    public class CognitoOptions
    {
        public string UserPoolId { get; set; } = default!;
        public string ClientId { get; set; } = default!;
    }
}
