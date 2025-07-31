
using FluentValidation;
using FluentValidation.Validators;

namespace CocktailsApp.Application.SeedWork
{
    public class GuidValidator<T> : PropertyValidator<T, Guid>
    {
        public override string Name => "GuidValidator";

        public override bool IsValid(ValidationContext<T> context, Guid value)
        {
            context.MessageFormatter.AppendArgument("PropertyName", context.PropertyPath);
            return value != Guid.Empty;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "'{PropertyName}' must be a valid non-empty GUID.";
    }
}
