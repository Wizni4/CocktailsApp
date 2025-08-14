using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Shared
{
    public sealed class GetUnitOfMeasureAuthorization
        : IAuthorize<GetUnitOfMeasureQuery>
    {
        public Task AuthorizeAsync(GetUnitOfMeasureQuery request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
