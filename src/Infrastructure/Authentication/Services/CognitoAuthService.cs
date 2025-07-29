using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

using Application.Authentication.Commands;

using CocktailsApp.Application.Authentication;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
using CocktailsApp.Infrastructure.SeedWork;

using Microsoft.Extensions.Options;

namespace CocktailsApp.Infrastructure.Authentication
{
    public class CognitoSettings
    {
        public string UserPoolId { get; set; } = default!;
        public string ClientId { get; set; } = default!;
    }

    public class CognitoAuthService(
        IAmazonCognitoIdentityProvider cognitoClient,
        IOptions<CognitoSettings> options,
        IUnitOfWork unitOfWork
    ) : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoClient = cognitoClient;
        private readonly string _userPoolId = options.Value.UserPoolId;
        private readonly string _clientId = options.Value.ClientId;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

        public async Task<SignInResponseDTO> SignInAsync(SignInCommand command)
        {
            var authRequest = new InitiateAuthRequest
            {
                ClientId = _clientId,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", command.Username },
                    { "PASSWORD", command.Password }
                },
            };

            var response = await _cognitoClient.InitiateAuthAsync(authRequest);

            if (response.AuthenticationResult == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            return new SignInResponseDTO
            {
                AccessToken = response.AuthenticationResult.AccessToken,
                IdToken = response.AuthenticationResult.IdToken,
                RefreshToken = response.AuthenticationResult.RefreshToken,
                //ExpiresIn = response.AuthenticationResult.ExpiresIn
            };
        }

        public async Task<SignInResponseDTO> RefreshTokenAsync(string refreshToken)
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

            return new SignInResponseDTO
            {
                AccessToken = response.AuthenticationResult.AccessToken,
                IdToken = response.AuthenticationResult.IdToken,
                RefreshToken = refreshToken, // AWS usually doesn't return a new one
                ExpiresIn = response.AuthenticationResult.ExpiresIn
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
