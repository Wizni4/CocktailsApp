namespace CocktailsApp.Domain.Common
{
    public interface IEntity
    {
        public Guid Id { get; }

        public DateTime CreationDate { get; }
        public DateTime UpdateDate { get; }
    }
}
