using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    public sealed class CreateRolesCommandValidator : ClubCommandValidator<CreateRolesCommand>
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
