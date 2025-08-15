

namespace CocktailsApp.ReadStore.Projections
{
    public interface IProjectionHandler<in TEvent>
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}
