using System.Reflection;


namespace CocktailsApp.Architecture.Tests.DesignRules.SeedWork
{
    [TestFixture]
    public abstract class BaseTests
    {
        protected Assembly? Assembly { get; set; }
        [SetUp]
        public abstract void Setup();
    }
}
