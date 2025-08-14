using CocktailsApp.Application.Common;
using FluentValidation;


namespace CocktailsApp.Application.Clubs
{
    public sealed class RolePermissionsUpdateModelValidator : AbstractValidator<RolePermissionsUpdateModel>
    {
        public RolePermissionsUpdateModelValidator()
        {
            RuleFor(c => c.Id).ValidGuid();
            RuleFor(c => c.Permissions).ValidList();
            RuleForEach(c => c.Permissions).ValidEnum();
        }
    }
}
