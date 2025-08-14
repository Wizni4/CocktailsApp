/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Domain.Ingredients;
using CocktailsApp.Infrastructure.SeedWork;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.CocktailAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="Cocktail"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class CocktailMap : EntityMap<Cocktail>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Cocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Cocktail"/> entity</param>
        public override void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(c => c.Description);
            builder.Property(c => c.Name)
                .IsRequired();

            // FK
            // -- CocktailIngredient
            builder.HasMany(c => c.Ingredients)
                .WithOne()
                .HasForeignKey("CocktailId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
