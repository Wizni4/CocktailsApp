using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    [TestFixture]
    public class EntitiesRulesTests : DomainRulesTests
    {
        private PredicateList Entities { get; set; }

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Entities = Types
                .InAssembly(Assembly)
                .That()
                .AreEntities();
        }

        [Test]
        public void Entities_ShouldBeSealed()
        {
            var result = Entities
             .Should()
             .BeSealed()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following entities are not sealed:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void Entities_ShouldNotImplementInterfaces()
        {
            var result = Entities
             .Should()
             .NotImplementInterfaces()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following entities inherit external interface:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void Entities_ShouldNotHavePublicMethods()
        {
            var result = Entities
             .Should()
             .NotHavePublicMethods()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following entities contains public methods:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void Entities_ShouldHaveImmutableAttributes()
        {
            var result = Entities
             .Should()
             .HaveImmutableAttributes()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following entities contains mutable properties:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }
    }
}
