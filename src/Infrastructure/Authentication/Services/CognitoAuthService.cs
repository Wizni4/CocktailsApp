using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

using CocktailsApp.Application.Authentication;

using Microsoft.Extensions.Options;

namespace CocktailsApp.Infrastructure.Authentication
{
    public class CognitoAuthService(
        IAmazonCognitoIdentityProvider cognitoClient,
        IOptions<CognitoSettings> options
    ) : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoClient = cognitoClient;
        private readonly string _userPoolId = options.Value.UserPoolId;
        private readonly string _clientId = options.Value.ClientId;

        public async Task<string> SignUpAsync(SignUpCommand command)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Username = command.Username,
                Password = command.Password,
                UserAttributes = new List<AttributeType>
                {
                    new() { Name = "name", Value = command.Username },
                    new() { Name = "email", Value = command.Email },
                }
            };

            var response = await _cognitoClient.SignUpAsync(signUpRequest);

            return response.UserSub;
        }

        public async Task<AuthDTO> SignInAsync(SignInCommand command)
        {
            var authRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = _userPoolId,
                ClientId = _clientId,
                AuthFlow = AuthFlowType.ADMIN_USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", command.Username },
                    { "PASSWORD", command.Password }
                },
            };

            var response = await _cognitoClient.AdminInitiateAuthAsync(authRequest);

            if (response.AuthenticationResult == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            return new AuthDTO
            {
                AccessToken = response.AuthenticationResult.AccessToken,
                IdToken = response.AuthenticationResult.IdToken,
                RefreshToken = response.AuthenticationResult.RefreshToken,
            };
        }

        public async Task<AuthDTO> RefreshTokenAsync(string refreshToken)
        {
            var refreshRequest = new InitiateAuthRequest
            {
                ClientId = _clientId,
                AuthFlow = AuthFlowType.REFRESH_TOKEN_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "REFRESH_TOKEN", refreshToken }
                }
            };

            var response = await _cognitoClient.InitiateAuthAsync(refreshRequest);

            if (response.AuthenticationResult == null)
                throw new UnauthorizedAccessException("Refresh failed");

            return new AuthDTO
            {
                AccessToken = response.AuthenticationResult.AccessToken,
                IdToken = response.AuthenticationResult.IdToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task SignOutAsync(SignOutCommand command)
        {
            await _cognitoClient.AdminUserGlobalSignOutAsync(new AdminUserGlobalSignOutRequest
            {
                UserPoolId = _userPoolId,
                Username = command.Username
            });
        }
    }
}
