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
    public interface IClubCommand : ICommand
    {
        Guid ClubId { get; }
    }
    public record ClubCommand<T>(Guid ClubId, Guid ActorId) : Command<T>(ActorId), IClubCommand;
}
