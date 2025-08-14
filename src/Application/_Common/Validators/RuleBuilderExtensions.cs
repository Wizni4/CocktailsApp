using FluentValidation;


namespace CocktailsApp.Application.Common
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

        public static IRuleBuilder<T, Address> ValidAddress<T>(this IRuleBuilder<T, Address> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AddressValidator());
        }
    }
}
