/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
/*
 * Infrastrucute namespaces
 */
using CocktailsApp.Infrastructure.SeedWork;
/*
* Framework namespaces
*/
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;


namespace CocktailsApp.Infrastructure.UserAggregate
{
    public class CognitoAuthenticationService(IAmazonCognitoIdentityProvider cognitoClient, string clientId, string userPoolId) : IAuthenticationService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoClient = cognitoClient;
        private readonly string _clientId = clientId;
        private readonly string _userPoolId = userPoolId;

        public async Task<string> SignInAsync(string username, string password)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Username = username,
                Password = password
            };

            var response = await _cognitoClient.SignUpAsync(signUpRequest);
            return response.UserSub;
        }

        public async Task<string> SignUpAsync(string username, string password)
        {
            var authRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = _userPoolId,
                ClientId = _clientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", username },
                    { "PASSWORD", password }
                }};

            var response = await _cognitoClient.AdminInitiateAuthAsync(authRequest);
            return response.AuthenticationResult.IdToken;
        }
    }
}
