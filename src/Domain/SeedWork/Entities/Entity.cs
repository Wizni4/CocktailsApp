/*
 * Framework namespaces
 */
using System;
using System.Collections.Generic;

namespace Domain.SeedWork
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
        /// <see cref="Entity"/> constructor that initialised the <see cref="Id"/> as a <see cref="Guid.NewGuid"/>.
        /// </summary>
        private protected Entity()
        {
        }
    }
}
