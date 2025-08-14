/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Domain.Users;
using CocktailsApp.Infrastructure.SeedWork;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="ClubCocktail"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubCocktailMap : EntityMap<ClubCocktail>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubCocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubCocktail"/> entity</param>
        public override void Configure(EntityTypeBuilder<ClubCocktail> builder)
        {
            base.Configure(builder);

            // FK
            // -- Cocktail
            builder.HasOne<Cocktail>()
                .WithMany()
                .HasForeignKey(cc => cc.CocktailId)
                .IsRequired();
        }
    }
}
