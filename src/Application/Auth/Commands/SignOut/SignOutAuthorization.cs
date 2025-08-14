using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Auth
{
    public sealed class SignOutAuthorization : IAuthorize<SignOutCommand>
    {
        public Task AuthorizeAsync(SignOutCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
