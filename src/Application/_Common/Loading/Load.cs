

namespace CocktailsApp.Application.Common
{
    public static class Load
    {
        public static ILoad<TEntity> For<TEntity>(string name) => new NamedLoadGraph<TEntity>(name);

        private sealed class NamedLoadGraph<TEntity>(
            string name
        ) : ILoad<TEntity>
        {
            public string Name { get; } = name;
            public override string ToString() => $"{typeof(TEntity).Name}:{Name}";
        }
    }
}
