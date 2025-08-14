using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public interface IClubCommand : ICommand
    {
        Guid ClubId { get; }
    }
    public abstract record ClubCommand<TResult>(Guid ClubId) : IClubCommand, ICommand<TResult>;
}
