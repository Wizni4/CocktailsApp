
namespace CocktailsApp.Domain.Common
{
    /// <summary>
    /// Defines a builder pattern for constructing an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="IFactory{T}"/> interface provides a contract for building complex objects step by step
    /// </remarks>
    /// <typeparam name="T">The type of object that the builder will construct.</typeparam>
    public interface IFactory<T>
    {
        /// <summary>
        /// Builds and returns the constructed object of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The constructed object of type <typeparamref name="T"/>.</returns>
        T Build();
    }
}
