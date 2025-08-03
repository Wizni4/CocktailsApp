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
using FluentValidation;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class PermissionsValidator<TCommand> : AbstractValidator<TCommand> where TCommand : IClubCommand
    {
        public PermissionsValidator(
            IClubRepository repository,
            IEnumerable<ClubPermissionType> requiredPermissions
        )
        {

            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var club = await repository.GetClubBydIdAsync(
                        command.ClubId,
                        opt => opt.Include(c => c.Members)
                                    .ThenInclude(m => m.Roles));

                    if (club != null)
                    {
                        // Try to get the member corresponding to the ActorId
                        var actor = club.Members.FirstOrDefault(new ClubMemberByIdSpecification(command.ActorId).SpecExpression.Compile());

                        if (actor != null)
                        {
                            // Get member permissions
                            var actorPermissions = actor.Roles
                                .SelectMany(m => m.Permissions);

                            // Get missing permissions
                            var missingPermissions = requiredPermissions.Where(rp => !actorPermissions.Any(p => new ClubPermissionByTypeSpecification(rp).SpecExpression.Compile()(p)));

                            // Add error if member does not have all required permissions
                            if (missingPermissions.Any())
                                context.AddFailure($"The user does not have the required permissions to perform the action.\nMissing permissions:\n- {string.Join("-\n ", missingPermissions)}");

                        }
                        else
                            context.AddFailure($"The specified actor is not part of the club: '{command.ActorId}'");
                    }
                });
        }
    }
}
