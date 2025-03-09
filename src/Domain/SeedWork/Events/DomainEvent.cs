/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.SeedWork
{
    /// <summary>
    /// <see langword="abstract"/> class that represents a domain event in the system, which is an event that signifies a change in the domain model.<br/>
    /// </summary>
    public abstract class DomainEvent
    {
        /// <summary>
        /// Gets the type of the <see cref="DomainEvent"/>
        /// </summary>
        public virtual string Type { get { return this.GetType().Name; } }

        /// <summary>
        /// Gets the <see cref="DateTime"/> of when the event was created.
        /// </summary>
        public virtual DateTime Created { get; private set; }

        /// <summary>
        /// <see cref="DomainEvent"/> properties.
        /// </summary>
        public virtual Dictionary<string, object> Args { get; private set; }

        /// <summary>
        /// Constructor to create a new instance of <see cref="DomainEvent"/>.
        /// </summary>
        public DomainEvent()
        {
            Created = DateTime.Now;
            Args = [];
        }

        /// <summary>
        /// Flattens the <see cref="DomainEvent"/>, adding all public properties to the <see cref="Args"/> collection.
        /// </summary>
        public void Flatten()
        {
            // Get all public properties of the object
            this.GetType()
               .GetProperties()
               .ToList()
               .ForEach(property => Add(property.Name, property.GetValue(this)));
        }

        /// <summary>
        /// Adds a property to the <see cref="Args"/> collection.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <returns>The current instance of the <see cref="DomainEvent"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyName"/> or <paramref name="propertyValue"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the property already exists in the <see cref="Args"/> collection.</exception>
        public DomainEvent Add(string propertyName, object? propertyValue)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(propertyName);
            ArgumentNullException.ThrowIfNull(propertyValue);

            if (Args.ContainsKey(propertyName))
                throw new ArgumentException($"Property {propertyName} is already added in {nameof(Args)}.", nameof(propertyName));

            Args.Add(propertyName, propertyValue);

            return this;
        }
    }
}
