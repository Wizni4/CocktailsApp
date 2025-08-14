using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Infrastructure.Common;


namespace CocktailsApp.Infrastructure.Clubs
{
    public sealed class ClubIncludesService : IIncludesService<Club>
    {
        public Func<IIncludable<Club>, IIncludable> GetIncludes(IEnumerable<ILoad<Club>> graph)
        {
            Func<IIncludable<Club>, IIncludable> result = opt => opt;
            foreach (var load in graph)
            {
                if (load.Name == ClubLoadGraphs.Cocktails.Name)
                    result = opt => result(opt.Include(c => c.Cocktails));

                if (load.Name == ClubLoadGraphs.Members.Name)
                    result = opt => result(opt.Include(c => c.Members));

                if (load.Name == ClubLoadGraphs.MembersWithRoles.Name)
                    result = opt => result(opt.Include(c => c.Members).ThenInclude(c => c.Roles));

                if (load.Name == ClubLoadGraphs.Roles.Name)
                    result = opt => result(opt.Include(c => c.Members).ThenInclude(c => c.Roles));
            }

            return result;
        }
    }
}
