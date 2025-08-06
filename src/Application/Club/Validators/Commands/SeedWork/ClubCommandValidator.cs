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

namespace CocktailsApp.Application.Club
{
    public class ClubCommandValidator<TCommand> : BaseValidator<TCommand> where TCommand : IClubCommand
    {
        public ClubCommandValidator(
            IClubRepository clubRepository,
            IEnumerable<ClubPermissionType>? requiredPermissions = null
        )
        {
            RuleFor(c => c.ActorId).ValidGuid();
            RuleFor(c => c.ClubId)
                .ValidGuid()
                .IsClubExists(clubRepository);

            if (requiredPermissions != null)
                RuleFor(c => c)
                    .HasPermissions(
                        clubRepository,
                        requiredPermissions
                    );
        }
    }
}
