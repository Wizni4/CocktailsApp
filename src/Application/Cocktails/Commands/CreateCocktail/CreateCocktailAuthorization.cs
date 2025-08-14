
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class CreateCocktailAuthorization
        : IAuthorize<CreateCocktailCommand>
    {
        public Task AuthorizeAsync(CreateCocktailCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if(!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
