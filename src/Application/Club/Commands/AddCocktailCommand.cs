/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using MediatR;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class AddCocktailCommand(Guid clubId, Guid cocktailId, Guid performingMemberId) : ICommand<ClubDTO>
    {
        public Guid CocktailId { get; } = cocktailId;
        public Guid ClubId { get; } = clubId;
        public Guid PerformingMemberId { get; } = performingMemberId;
    }
}
