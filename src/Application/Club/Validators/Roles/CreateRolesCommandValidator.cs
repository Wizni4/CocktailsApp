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
    public class CreateRoleValidator : AbstractValidator<CreateRolesCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
            RuleFor(c => c.ActorId).ValidGuid();
        }
    }
}
