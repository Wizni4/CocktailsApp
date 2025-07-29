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
    public class CreateRoleWithOptionsCommandValidator : AbstractValidator<CreateRoleWithOptionsCommand>
    {
        public CreateRoleWithOptionsCommandValidator()
        {
            RuleFor(c => c.ClubId)
                .NotEqual(Guid.Empty)
                .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleName)
                .NotNull()
                .WithMessage("RoleName must be provided.")
                .NotEmpty()
                .WithMessage("RoleName cannot be empty.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");

            When(c => c.Permissions is not null, () =>
            {
                RuleForEach(c => c.Permissions)
                    .IsInEnum()
                    .WithMessage("Permission must be a valid enum value.");
            });
        }
    }
}
