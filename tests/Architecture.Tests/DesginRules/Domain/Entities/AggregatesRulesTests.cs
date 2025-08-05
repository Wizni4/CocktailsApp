using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    [TestFixture]
    public class AggregatesRulesTests : DomainRulesTests
    {
        private PredicateList Aggregates { get; set; }

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Aggregates = Types
                .InAssembly(Assembly)
                .That()
                .AreAggregates();
        }

        [Test]
        public void Aggregates_ShouldBeSealed()
        {
            // Act
            var result = Aggregates
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            Assert.True(
                result.IsSuccessful,
                "The following aggregates are not sealed:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void Aggregates_ShouldNotImplementInterfaces()
        {
            var result = Aggregates
             .Should()
             .NotImplementInterfaces()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following aggregates inherit external interface:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void Aggregates_ShouldHaveImmutableAttributes()
        {
            var result = Aggregates
             .Should()
             .HaveImmutableAttributes()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following aggregates contains mutable properties:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void Aggregate_ShouldHaveInternalConstructor()
        {
            var result = Aggregates
             .Should()
             .HaveInternalConstructor()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following aggregate contains public constructor:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }
    }
}
