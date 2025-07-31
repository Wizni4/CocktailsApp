

using FluentValidation;

namespace CocktailsApp.Application.SeedWork
{
    public class EnumValidator<T, TEnum> : FluentValidation.Validators.EnumValidator<T, TEnum>
    {
        public override string Name => "EnumValidator";

        public override bool IsValid(ValidationContext<T> context, TEnum value)
        {
            context.MessageFormatter.AppendArgument("PropertyName", context.PropertyPath);
            return base.IsValid(context, value);
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "'{PropertyName}' must be a valid enum value.";
    }
}
