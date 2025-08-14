

namespace CocktailsApp.Application.Common
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        string? Username { get; }
        Guid UserId { get; }
        Guid? SessionId { get; }
    }
}
