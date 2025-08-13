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
    public class CreateRolesCommandValidator : ClubCommandValidator<CreateRolesCommand>
    {
        public CreateRolesCommandValidator()
        {
            RuleFor(c => c.NewRoles).ValidList();
            RuleForEach(c => c.NewRoles)
                 .ChildRules(a =>
                 {
                     a.RuleFor(r => r.Name).ValidString();
                     a.When(r => r.Permissions is not null, () =>
                     {
                         a.RuleFor(m => m.Permissions!).ValidList();
                         a.RuleForEach(m => m.Permissions).ValidEnum();
                     });
                 });
        }
    }
}
