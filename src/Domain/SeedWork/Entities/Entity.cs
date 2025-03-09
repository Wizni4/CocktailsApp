/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.SeedWork
{
    /// <summary>
    /// <see langword="abstract"/> class that represent an <see langword="object"/> with it own identity,
    /// defined by the <see cref="Id"/> as a unmutable <see cref="Guid"/> and initialised by the <see cref="Entity"/> constructor.<br/>
    /// It's a persitent and mutable <see langword="object"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="Id"/> can be <see langword="override"/>.
    /// </remarks>
    public abstract class Entity : BaseClass
    {
        /// <summary>
        /// Unmutable <see cref="Entity"/> identity.
        /// </summary>
        public virtual Guid Id { get; }

        /// <summary>
        /// Constructor to create a new instance of <see cref="Entity"/>.
        /// </summary>
        private protected Entity()
        {
        }
    }
}
