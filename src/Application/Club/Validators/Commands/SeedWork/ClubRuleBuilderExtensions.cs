
using CocktailsApp.Application.SeedWork;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using FluentValidation;
using FluentValidation.Validators;

using System;
using CocktailsApp.Domain.ClubAggregate;

namespace CocktailsApp.Application.Club
{
    public static class ClubRuleBuilderExtensions
    {
        public static IRuleBuilder<T, Guid> IsClubExists<T>(this IRuleBuilder<T, Guid> ruleBuilder, IRepository<DomainClub> clubRepository)
        {
            return ruleBuilder.SetValidator(new ClubExistsValidator(clubRepository));
        }

        public static IRuleBuilder<TCommand, TCommand> HasPermissions<TCommand>(
            this IRuleBuilder<TCommand, TCommand> ruleBuilder,
            IClubRepository clubRepository,
            IEnumerable<ClubPermissionType> requiredPermissions) where TCommand : IClubCommand
        {
            return ruleBuilder.SetValidator(new PermissionsValidator<TCommand>(clubRepository, requiredPermissions));
        }
    }
}
