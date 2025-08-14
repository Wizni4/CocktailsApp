using MediatR;


namespace CocktailsApp.Application.Common
{
    public interface IAuthorize<in TRequest> where TRequest : IBaseRequest
    {
        Task AuthorizeAsync(TRequest request, ICurrentUser user, CancellationToken cancellationToken);
    }
}
