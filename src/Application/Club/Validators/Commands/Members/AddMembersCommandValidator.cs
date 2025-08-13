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
using CocktailsApp.Application.User;
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;


namespace CocktailsApp.Application.Club
{
    public class AddMembersCommandValidator : ClubCommandValidator<AddMembersCommand>
    {
        public AddMembersCommandValidator()
        {
            RuleFor(c => c.NewMembers).ValidList();
            RuleForEach(c => c.NewMembers)
                .ChildRules(a =>
                {
                    a.RuleFor(m => m.UserId).ValidGuid();
                    a.When(m => m.RoleIds is not null, () =>
                    {
                        a.RuleForEach(m => m.RoleIds).ValidGuid();
                    });
                });
        }
    }
}
