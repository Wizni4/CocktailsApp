namespace CocktailsApp.Application.Common
{
    public interface IIdempotentCommand : ICommand
    {
        Guid RequestId { get; }
        string IdempotencyKey { get; }
    }
}
