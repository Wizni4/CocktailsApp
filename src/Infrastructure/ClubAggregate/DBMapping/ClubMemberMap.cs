/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.UserAggregate;
using CocktailsApp.Infrastructure.SeedWork;



/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="ClubMember"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubMemberMap : EntityMap<ClubMember>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubMember"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubMember"/> entity</param>
        public override void Configure(EntityTypeBuilder<ClubMember> builder)
        {
            base.Configure(builder);

            // FK
            // - ClubRole (Many-to-Many)
            builder.HasMany(cm => cm.Roles)
                .WithMany()
                .UsingEntity<ClubMemberToClubRole>(
                    "ClubMember_Roles",
                    j => j
                        .HasOne(c => c.ClubRole)
                        .WithMany()
                        .HasForeignKey("ClubRoleId")
                        .OnDelete(DeleteBehavior.ClientCascade),
                    j => j
                        .HasOne(c => c.ClubMember)
                        .WithMany()
                        .HasForeignKey("ClubMemberId")
                        .OnDelete(DeleteBehavior.ClientCascade),
                    j =>
                    {
                        j.HasKey("ClubRoleId", "ClubMemberId");
                    });

            // - USer
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(cm => cm.UserId)
                .IsRequired();

        }
    }
}
