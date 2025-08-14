using FluentValidation;
using FluentValidation.Validators;

namespace CocktailsApp.Application.Common
{
    public class StringValidator<T> : PropertyValidator<T, string?>
    {
        public override string Name => "StringValidator";

        public override bool IsValid(ValidationContext<T> context, string? value)
        {
            context.MessageFormatter.AppendArgument("PropertyName", context.PropertyPath);
            return !string.IsNullOrWhiteSpace(value);
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "'{PropertyName}' must be provided and cannot be empty.";
    }
}
