
using CocktailsApp.Application.SeedWork;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using FluentValidation;
using FluentValidation.Validators;

using System;

namespace CocktailsApp.Application.Club
{
    public static class ClubRuleBuilderExtensions
    {
        public static IRuleBuilder<T, Guid> IsClubExists<T>(this IRuleBuilder<T, Guid> ruleBuilder, IRepository<DomainClub> clubRepository)
        {
            return ruleBuilder.SetValidator(new ClubExistsValidator(clubRepository));
        }
    }
}
