/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.CocktailAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="CocktailIngredient"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class CocktailIngredientMap : IEntityTypeConfiguration<CocktailIngredient>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Cocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Cocktail"/> entity</param>
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            // PK
            builder.HasKey(ci => ci.Id);

            // Properties
            builder.Property(ci => ci.Quantity);
            builder.Property(ci => ci.CreationDate);
            builder.Property(ci => ci.UpdateDate);

            // Value objects
            builder.ComplexProperty(ci => ci.Ingredient, a =>
            {
                a.IsRequired();

                a.Property(i => i.Name).IsRequired();
                a.Property(i => i.BaseUnit).IsRequired();
            });
        }

    }
}
