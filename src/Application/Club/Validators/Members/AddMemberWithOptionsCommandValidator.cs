/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using FluentValidation;

namespace CocktailsApp.Application.Club
{
    internal class AddMemberWithOptionsCommandValidator : AbstractValidator<AddMemberWithOptionsCommand>
    {
        public AddMemberWithOptionsCommandValidator()
        {
            RuleFor(c => c.ClubId)
                .NotEqual(Guid.Empty)
                .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.NewMemberUserId)
                 .NotEqual(Guid.Empty)
                 .WithMessage("Each NewMemberUserId must be a valid non-empty GUID.");           

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");

            When(c => c.RoleIds is not null, () =>
            {
                RuleForEach(c => c.RoleIds)
                    .NotEqual(Guid.Empty)
                    .WithMessage("RoleId must be a valid non-empty GUID.");
            });

            When(c => c.Permissions is not null, () =>
            {
                RuleForEach(c => c.Permissions)
                    .IsInEnum()
                    .WithMessage("Permission must be a valid enum value.");
            });
        }
    }
}
