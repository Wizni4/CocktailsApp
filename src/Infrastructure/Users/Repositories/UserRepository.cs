/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.UserAggregate;
/*
* Framework namespaces
*/
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

namespace Infrastructure.Users
{
    public class UserRepository(string cognitoPoolId, string cognitoClientId) : IUserRepository
    {
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient = new();
        private readonly string _cognitoPoolId = cognitoPoolId;
        private readonly string _cognitoClientId = cognitoClientId;

        public async Task<bool> AuthenticateUserAsync(User entity)
        {
            var authRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = _cognitoPoolId,
                ClientId = _cognitoClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", entity.Name },
                    { "PASSWORD", entity.Password }
                }
            };

            // Authenticate the user in Cognito
            var authResponse = await _cognitoClient.AdminInitiateAuthAsync(authRequest);
            return authResponse.AuthenticationResult != null;
        }

        public async void Create(User entity)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _cognitoClientId,
                Username = entity.Name,
                Password = entity.Password,
                UserAttributes =
                [
                    new AttributeType { Name = "email", Value = entity.Email }
                ]
            };

            // Sign up the user in Cognito User Pool
            _ = await _cognitoClient.SignUpAsync(signUpRequest);
        }

        public void CreateRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> ReadAllAsync(Func<IIncludable<User>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public async Task<User> ReadAsync(ISpecification<User> spec, Func<IIncludable<User>, IIncludable>? includes = null)
        {
            var request = new GetUserRequest
            {
                AccessToken = token
            };

            // Get the user associated with the token
            var response = await _cognitoClient.GetUserAsync(request);
            return ;
        }

        public Task<IEnumerable<User>> ReadRangeAsync(ISpecification<User> spec, Func<IIncludable<User>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
