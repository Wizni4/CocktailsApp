namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validates the <see cref="DeleteClubCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public sealed class DeleteClubCommandValidator : ClubCommandValidator<DeleteClubCommand>;
}
