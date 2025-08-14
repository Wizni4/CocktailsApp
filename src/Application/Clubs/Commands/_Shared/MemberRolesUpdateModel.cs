namespace CocktailsApp.Application.Clubs
{
    public sealed record MemberRolesUpdateModel(
        Guid Id,
        IEnumerable<Guid> RoleIds
    );
}
