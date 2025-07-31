
using FluentValidation;
using FluentValidation.Validators;

namespace CocktailsApp.Application.SeedWork
{
    public class ListValidator<T, TProperty> : PropertyValidator<T, IEnumerable<TProperty>>
    {
        public override string Name => "ListValidator";

        public override bool IsValid(ValidationContext<T> context, IEnumerable<TProperty> value)
        {
            context.MessageFormatter.AppendArgument("PropertyName", context.PropertyPath);
            return value != null && value.Any();
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "'{PropertyName}' must be provided and cannot be empty.";
    }
}
