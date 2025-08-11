using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class LoadClubWithCocktailsCommandSpecification(Guid clubId) : ClubCommandSpecification(clubId)
    {
        public override Func<IIncludable<Domain.ClubAggregate.Club>, IIncludable> Includes
        {
            get
            {
                return opt =>
                {
                    var query = opt.Include(c => c.Cocktails);
                    return base.Includes!(query);
                };
            }
        }
    }
}
