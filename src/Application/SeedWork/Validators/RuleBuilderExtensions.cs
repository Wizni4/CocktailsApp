
using CocktailsApp.Application.Shared;

using FluentValidation;
using FluentValidation.Validators;

using System;

namespace CocktailsApp.Application.SeedWork
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string?> ValidString<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new StringValidator<T>());
        }

        public static IRuleBuilder<T, Guid> ValidGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new GuidValidator<T>());
        }

        public static IRuleBuilder<T, TEnum> ValidEnum<T, TEnum>(this IRuleBuilder<T, TEnum> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new EnumValidator<T, TEnum>());
        }

        public static IRuleBuilderOptions<T, IEnumerable<TProperty>> ValidList<T, TProperty>(
                this IRuleBuilder<T, IEnumerable<TProperty>> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ListValidator<T, TProperty>());
        }

        public static IRuleBuilder<T, AddressDTO> ValidAddress<T>(this IRuleBuilder<T, AddressDTO> ruleBuilder) 
        {
            return ruleBuilder.SetValidator(new AddressValidator());
        }
    }
}
