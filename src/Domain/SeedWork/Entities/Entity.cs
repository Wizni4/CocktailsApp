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
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Unmutable <see cref="Entity"/> identity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Guid Id { get; }
        public Guid CreatedBy { get => _createdBy; }
        private protected Guid _createdBy;

        /// <summary>
        /// <see cref="Entity"/> date of creation.
        /// </summary>
        public DateTime CreationDate { get => _creationDate; }
        private readonly DateTime _creationDate;
        /// <summary>
        /// <see cref="Entity"/> last date of update.
        /// </summary>
        public DateTime UpdateDate { get => _updateDate; }
        private DateTime _updateDate;

        private protected Entity() { }

        /// <summary>
        /// Constructor to create a new instance of <see cref="Entity"/>.
        /// </summary>
        private protected Entity(Guid createdBy)
        {
            if (createdBy == Guid.Empty)
                throw new ArgumentException("User creating entity must be specified");

            _createdBy = createdBy;
            Id = Guid.NewGuid();
            _creationDate = DateTime.UtcNow;
            _updateDate = _creationDate;
        }

        /// <summary>
        /// Update the <see cref="UpdateDate"/> to <see cref="DateTime.UtcNow"/>
        /// </summary>
        private protected void Touch()
        {
            _updateDate = DateTime.UtcNow;
        }
    }
}
