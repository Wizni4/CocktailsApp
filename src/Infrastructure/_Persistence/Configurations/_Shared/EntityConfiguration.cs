using CocktailsApp.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.Persistence
{
    public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // PK
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.CreationDate)
                .IsRequired();
            builder.Property(e => e.CreatedBy)
                .IsRequired();
            builder.Property(e => e.UpdateDate)
                .IsRequired();
            builder.Property(e => e.UpdatedBy)
                .IsRequired();
            builder.Property(e => e.ImageId);
        }
    }
}
