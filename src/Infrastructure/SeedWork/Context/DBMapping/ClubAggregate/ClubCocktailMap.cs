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
    /// Represents the configuration for the <see cref="ClubCocktail"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubCocktailMap : IEntityTypeConfiguration<ClubCocktail>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubCocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubCocktail"/> entity</param>
        public void Configure(EntityTypeBuilder<ClubCocktail> builder)
        {
            // PK
            builder.HasKey(cc => cc.Id);

            // Properties

            // FK
            builder.HasOne<Cocktail>()
                .WithMany()
                .HasForeignKey(cc => cc.CocktailId);
        }
    }
}
