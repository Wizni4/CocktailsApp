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
    public class ClubBaseValidator<TCommand> : BaseValidator<TCommand> where TCommand : IClubCommand
    {
        public ClubBaseValidator(
            IClubRepository clubRepository,
            IEnumerable<ClubPermissionType>? requiredPermissions = null
        )
        {
            RuleFor(c => c.ActorId).ValidGuid();
            RuleFor(c => c.ClubId)
                .ValidGuid()
                .IsClubExists(clubRepository);

            if(requiredPermissions != null) 
                RuleFor(c => c)
                    .HasPermissions(
                        clubRepository,
                        requiredPermissions
                    );
        }
    }
}
