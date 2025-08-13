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
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="UpdateRolesCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateRolesCommandValidator : ClubCommandValidator<UpdateRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateRolesCommand"/>.
        /// </summary>
        public UpdateRolesCommandValidator()
        {
            RuleFor(c => c.Roles).ValidList();
            RuleForEach(c => c.Roles)
                .ChildRules(a =>
                {
                    a.RuleFor(r => r.Id).ValidGuid();
                    a.When(r => r.Permissions is not null, () =>
                    {
                        a.RuleFor(r => r.Permissions!).ValidList();
                        a.RuleForEach(r => r.Permissions).ValidEnum();
                    });
                });
        }
    }
}
