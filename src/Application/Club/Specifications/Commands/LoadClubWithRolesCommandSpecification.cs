using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class LoadClubWithRolesCommandSpecification(Guid clubId) : ClubCommandSpecification(clubId)
    {
        public override Func<IIncludable<Domain.ClubAggregate.Club>, IIncludable>? Includes
        {
            get
            {
                return opt =>
                {
                    var query = opt.Include(c => c.Roles);
                    return base.Includes!(query);
                };
            }
        }
    }
}
