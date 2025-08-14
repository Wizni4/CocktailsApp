using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

using CocktailsApp.Application.Auth;

using Microsoft.Extensions.Options;


namespace CocktailsApp.Infrastructure.Auth
{
    public class CognitoAuthService(
        IAmazonCognitoIdentityProvider cognitoClient,
        IOptions<CognitoOptions> options
    ) : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoClient = cognitoClient;
        private readonly string _userPoolId = options.Value.UserPoolId;
        private readonly string _clientId = options.Value.ClientId;

        public async Task<string> SignUpAsync(SignUpCommand command, CancellationToken cancellationToken)
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

        public async Task<AuthTokens> SignInAsync(SignInCommand command, CancellationToken cancellationToken)
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

            return new AuthTokens
            (
                response.AuthenticationResult.AccessToken,
                response.AuthenticationResult.IdToken,
                response.AuthenticationResult.RefreshToken
            );
        }

        public async Task<AuthTokens> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
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

            var response = await _cognitoClient.InitiateAuthAsync(
                refreshRequest,
                cancellationToken);

            if (response.AuthenticationResult == null)
                throw new UnauthorizedAccessException("Refresh failed");

            return new AuthTokens
            (
                response.AuthenticationResult.AccessToken,
                response.AuthenticationResult.IdToken,
                response.AuthenticationResult.RefreshToken
            );
        }

        public async Task SignOutAsync(AuthSession session, CancellationToken cancellationToken)
        {
            await _cognitoClient.AdminUserGlobalSignOutAsync(new AdminUserGlobalSignOutRequest
            {
                UserPoolId = _userPoolId,
                Username = session.Username,
            });
        }
    }
}
