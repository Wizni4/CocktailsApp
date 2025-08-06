/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */
using MediatR;


namespace CocktailsApp.Application.Club
{
    public interface IClubCommand : IBaseRequest
    {
        Guid ClubId { get; }
        Guid ActorId { get; }
    }
    public record ClubCommand<T>(Guid ClubId, Guid ActorId) : ICommand<T>, IClubCommand;
}
