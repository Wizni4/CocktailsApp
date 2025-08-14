using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    public sealed class MemberRolesUpdateModelValidator : AbstractValidator<MemberRolesUpdateModel>
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
