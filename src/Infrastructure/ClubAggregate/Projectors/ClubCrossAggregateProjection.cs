// Infrastructure/Read/Projections/CrossAggregateProjection.cs
using AutoMapper;

using CocktailsApp.Application.Clubs;
using CocktailsApp.Infrastructure.SeedWork;



namespace CocktailsApp.Infrastructure.ClubAggregate
{
    public sealed class ClubCrossAggregateProjection : IProjection
    {
        public IProjection Inner { get; }

        public ClubCrossAggregateProjection(IMapper mapper)
        {
            Inner = new Projection<ClubRead>(mapper);
            // User rename patches all clubs that contain the user
            //.UpdateManyOn<UserRenamed>(
            //    (db, e) => db.Clubs.Where(c => c.Members.Any(m => m.UserId == e.UserId)),
            //    (c, e) =>
            //    {
            //        foreach (var m in c.Members.Where(m => m.UserId == e.UserId))
            //            m.Username = e.Username;
            //    })
            // Club member added -> set username

            // Cocktail renamed/image updated → patch all clubs containing the cocktail
            //.UpdateManyOn<CocktailRenamedEvent>(
            //    (db, e) => db.Set<ClubRead>().Where(c => c.Cocktails.Any(k => k.CocktailId == e.CocktailId)),
            //    (c, e) =>
            //    {
            //        foreach (var k in c.Cocktails.Where(k => k.CocktailId == e.CocktailId))
            //            k.Name = e.Name;
            //    })
            //.UpdateManyOn<CocktailDescriptionChangedEvent>(
            //    (db, e) => db.Set<ClubRead>().Where(c => c.Cocktails.Any(k => k.CocktailId == e.CocktailId)),
            //    (c, e) =>
            //    {
            //        foreach (var k in c.Cocktails.Where(k => k.CocktailId == e.CocktailId))
            //            k.Description = e.Description;
            //    })
            //.UpdateManyOn<CocktailImageUpdated>(
            //    (db, e) => db.Clubs.Where(c => c.Cocktails.Any(k => k.CocktailId == e.CocktailId)),
            //    (c, e) =>
            //    {
            //        foreach (var k in c.Cocktails.Where(k => k.CocktailId == e.CocktailId))
            //            k.ImageId = e.ImageId;
            //    })

            // Ingredients added/removed on cocktail stream
            //.UpdateManyOn<IngredientAddedEvent>(
            //    (db, e) => db.Clubs.Where(c => c.Cocktails.Any(k => k.CocktailId == e.CocktailId)),
            //    (c, e) =>
            //    {
            //        var cocktail = c.Cocktails.First(k => k.CocktailId == e.CocktailId);
            //        if (!cocktail.Ingredients.Any(i => i.IngredientId == e.IngredientId))
            //        {
            //            cocktail.Ingredients.Add(new ClubCocktailIngredientRead
            //            {
            //                IngredientId = e.IngredientId,
            //                Name = e.Name,
            //                Type = e.Type,
            //                IsAlcoholic = e.IsAlcoholic,
            //                Quantity = e.Quantity,
            //                Unit = e.Unit,
            //                ImageId = e.ImageId,
            //                Allergens = e.Allergens ?? new()
            //            });
            //        }
            //    })
            //.UpdateManyOn<IngredientRemovedEvent>(
            //    (db, e) => db.Clubs.Where(c => c.Cocktails.Any(k => k.CocktailId == e.CocktailId)),
            //    (c, e) =>
            //    {
            //        foreach (var k in c.Cocktails.Where(k => k.CocktailId == e.CocktailId))
            //            k.Ingredients.RemoveAll(i => i.IngredientId == e.IngredientId);
            //    })

            //// Ingredient renamed → patch all clubs with any cocktail containing it
            //.UpdateManyOn<IngredientRenamed>(
            //    (db, e) => db.Clubs.Where(c => c.Cocktails.Any(k => k.Ingredients.Any(i => i.IngredientId == e.IngredientId))),
            //    (c, e) =>
            //    {
            //        foreach (var k in c.Cocktails)
            //            foreach (var i in k.Ingredients.Where(i => i.IngredientId == e.IngredientId))
            //                i.Name = e.Name;
            //    });
        }



        public bool CanHandle(Type eventType) => Inner.CanHandle(eventType);

        public Task HandleAsync(object @event, EFReadDbContext db, CancellationToken cancellationToken)
            => Inner.HandleAsync(@event, db, cancellationToken);
    }
}
