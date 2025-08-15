
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Users;
using CocktailsApp.Infrastructure.Common;

namespace CocktailsApp.Infrastructure.Users
{
    public sealed class UserIncludesService : IIncludesService<User>
    {
        public Func<IIncludable<User>, IIncludable> GetIncludes(IEnumerable<ILoad<User>> graph)
        {
            throw new NotImplementedException();
        }
    }
}
