

namespace CocktailsApp.Application.Common
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        string? Username { get; }
        Guid UserId { get; }
        Guid? SessionId { get; }
        Guid UserIdOrThrow();
    }
}
