using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public static class ClubLoadGraphs
    {
        public static readonly ILoad<Club> Default = Load.For<Club>("Default");
        public static readonly ILoad<Club> Cocktails = Load.For<Club>("Cocktails");
        public static readonly ILoad<Club> Members = Load.For<Club>("Members");
        public static readonly ILoad<Club> MembersWithRoles = Load.For<Club>("MembersWithRoles");
        public static readonly ILoad<Club> Roles = Load.For<Club>("Roles");
    }
}
