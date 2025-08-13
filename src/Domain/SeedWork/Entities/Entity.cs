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

        public Guid UpdatedBy { get => _updatedBy; }
        private protected Guid _updatedBy;

        /// <summary>
        /// <see cref="Entity"/> last date of update.
        /// </summary>
        public DateTime UpdateDate { get => _updateDate; }
        private DateTime _updateDate;

        public string? ImageId { get => _imageId; }
        public string? _imageId = null;

        private protected Entity() { }

        /// <summary>
        /// Constructor to create a new instance of <see cref="Entity"/>.
        /// </summary>
        private protected Entity(Guid createdBy)
        {
            if (createdBy == Guid.Empty)
                throw new ArgumentException("User creating entity must be specified");

            Id = Guid.NewGuid();
            _createdBy = createdBy;
            _creationDate = DateTime.UtcNow;

            _updateDate = _creationDate;
            _updatedBy = _createdBy;
        }

        public void UpdateImage(string imageId, Guid actorId)
        {
            _imageId = imageId;
            Touch(actorId);
        }

        /// <summary>
        /// Update the <see cref="UpdateDate"/> to <see cref="DateTime.UtcNow"/>
        /// </summary>
        private protected void Touch(Guid actorId)
        {
            _updateDate = DateTime.UtcNow;
            _updatedBy = actorId;
        }
    }
}
