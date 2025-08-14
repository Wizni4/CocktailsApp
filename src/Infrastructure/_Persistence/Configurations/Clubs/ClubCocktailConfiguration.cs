using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Infrastructure.Common;

using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the configuration for the <see cref="ClubCocktail"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubCocktailConfiguration : EntityConfiguration<ClubCocktail>
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
