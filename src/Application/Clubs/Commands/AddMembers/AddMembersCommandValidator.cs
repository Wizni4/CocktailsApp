using CocktailsApp.Application.Common;

using FluentValidation;


namespace CocktailsApp.Application.Clubs
{
    public sealed class AddMembersCommandValidator : ClubCommandValidator<AddMembersCommand>
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
