

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class DeleteCocktailAuthorization
        : IAuthorize<DeleteCocktailCommand>
    {
        public Task AuthorizeAsync(DeleteCocktailCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
