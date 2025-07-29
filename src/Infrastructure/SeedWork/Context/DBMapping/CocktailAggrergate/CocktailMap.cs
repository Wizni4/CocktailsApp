/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.UserAggregate;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents the configuration for the <see cref="Cocktail"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class CocktailMap : IEntityTypeConfiguration<Cocktail>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Cocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Cocktail"/> entity</param>
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            // PK
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Name);

            // Value objects
            builder.OwnsMany(c => c.Ingredients);
        }
    }
}
