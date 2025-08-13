/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    public class ClubCommandValidator<TCommand> : CommandValidator<TCommand> where TCommand : Command, IClubCommand
    {
        public ClubCommandValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
        }
    }
}
