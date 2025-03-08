/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    public abstract class DomainEvent
    {
        public virtual string Type { get { return this.GetType().Name; } }

        public virtual DateTime Created { get; private set; }

        public virtual Dictionary<string, object> Args { get; private set; }

        public DomainEvent()
        {
            Created = DateTime.Now;
            Args = [];
        }

        public void Flatten()
        {
            // Get all public properties of the object
            this.GetType()
               .GetProperties()
               .ToList()
               .ForEach(property => Add(property.Name, property.GetValue(this)));
        }

        public DomainEvent Add(string propertyName, object? propertyValue)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            ArgumentNullException.ThrowIfNull(propertyValue);

            if (Args.ContainsKey(propertyName))
                throw new ArgumentException($"Property {propertyName} is already added in {nameof(Args)}.", nameof(propertyName));

            Args.Add(propertyName, propertyValue);

            return this;
        }
    }
}
