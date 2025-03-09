/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    /// <summary>
    /// <see langword="abstract"/> base class of all other classes.
    /// </summary>
    public abstract class BaseClass
    {
        /// <summary>
        /// Type name of a <see cref="BaseClass"/> based type.
        /// </summary>
        /// <remarks>
        /// Property is not persisted.
        /// </remarks>
        private protected string Type { get { return this.GetType().Name; } }
    }
}
