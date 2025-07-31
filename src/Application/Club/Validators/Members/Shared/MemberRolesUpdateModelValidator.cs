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
    public class MemberRolesUpdateModelValidator : AbstractValidator<MemberRolesUpdateModel>
    {
        public MemberRolesUpdateModelValidator()
        {
            RuleFor(m => m.Id).ValidGuid();
            When(m => m.RoleIds is not null, () =>
            {
                RuleForEach(c => c.RoleIds).ValidGuid();
            });
        }
    }
}
