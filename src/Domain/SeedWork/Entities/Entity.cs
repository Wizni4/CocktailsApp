/*
 * Framework namespaces
 */

using System.ComponentModel.DataAnnotations.Schema;

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
    public abstract class Entity
    {
        /// <summary>
        /// Unmutable <see cref="Entity"/> identity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; }

        /// <summary>
        /// <see cref="Entity"/> date of creation.
        /// </summary>
        public DateTime CreationDate { get; }
        /// <summary>
        /// <see cref="Entity"/> last date of update.
        /// </summary>
        public DateTime UpdateDate { get => _updateDate; }
        private DateTime _updateDate = DateTime.UtcNow;

        /// <summary>
        /// Constructor to create a new instance of <see cref="Entity"/>.
        /// </summary>
        private protected Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Update the <see cref="UpdateDate"/> to <see cref="DateTime.UtcNow"/>
        /// </summary>
        public void RefreshUpdateDate()
        {
            _updateDate = DateTime.UtcNow;
        }
    }
}
