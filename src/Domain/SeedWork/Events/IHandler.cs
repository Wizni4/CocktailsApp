/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.SeedWork
{
    /// <summary>
    /// Interface that represents ageneric <see cref="DomainEvent"/> handler.
    /// </summary>
    /// <typeparam name="T"><see cref="DomainEvent"/> model type</typeparam>
    public interface IHandler<T> where T : DomainEvent
    {
        /// <summary>
        /// Handle a <see cref="DomainEvent"/>
        /// </summary>
        /// <param name="args"><see cref="DomainEvent"/> to handle</param>
        void Handle(T args);
    }
}
