// Infrastructure/SeedWork/Projection.cs

using AutoMapper;

using CocktailsApp.Infrastructure.SeedWork;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.Infrastructure.SeedWork
{

    /// <summary>
    /// AutoMapper-first projection builder with async hooks when you need DB lookups.
    /// Use Map* for simple patches; use *Async when you must query EFReadDbContext (e.g., fill Username/CocktailName).
    /// </summary>
    public sealed class Projection<TRead> : IProjection where TRead : class, new()
    {
        private readonly IMapper _mapper;
        private readonly List<IHandler> _handlers = new();

        public IProjection Inner { get; } = default!;

        public Projection(IMapper mapper) => _mapper = mapper;

        // ----------- DSL -----------

        // Create doc from event (idempotent) using AutoMapper: mapper.Map<TRead>(e)
        public Projection<TRead> CreateMapOn<TEvent>(Func<TEvent, object> key)
        {
            _handlers.Add(new CreateMapHandler<TEvent>(_mapper, key));
            return this;
        }

        // Update one doc by mapping event -> existing doc: mapper.Map(e, doc)
        public Projection<TRead> UpdateMapOn<TEvent>(Func<TEvent, object> key)
        {
            _handlers.Add(new UpdateMapHandler<TEvent>(_mapper, key));
            return this;
        }

        // Update many docs selected by a query: foreach doc => mapper.Map(e, doc)
        public Projection<TRead> UpdateMapManyOn<TEvent>(Func<EFReadDbContext, TEvent, IQueryable<TRead>> selector)
        {
            _handlers.Add(new UpdateMapManyHandler<TEvent>(_mapper, selector));
            return this;
        }

        // Update one doc with an async lambda that has access to the DB (use when you must look up username/cocktail)
        public Projection<TRead> UpdateOnAsync<TEvent>(
            Func<TEvent, object> key,
            Func<TRead, TEvent, EFReadDbContext, CancellationToken, Task> applyAsync)
        {
            _handlers.Add(new UpdateAsyncHandler<TEvent>(key, applyAsync));
            return this;
        }

        // Update many docs with an async lambda + selector
        public Projection<TRead> UpdateManyOnAsync<TEvent>(
            Func<EFReadDbContext, TEvent, IQueryable<TRead>> selector,
            Func<TRead, TEvent, EFReadDbContext, CancellationToken, Task> applyAsync)
        {
            _handlers.Add(new UpdateManyAsyncHandler<TEvent>(selector, applyAsync));
            return this;
        }

        // Delete one doc by key (idempotent)
        public Projection<TRead> DeleteOn<TEvent>(Func<TEvent, object> key)
        {
            _handlers.Add(new DeleteHandler<TEvent>(key));
            return this;
        }

        // ----------- IProjection -----------

        public bool CanHandle(Type eventType) => _handlers.Any(h => h.EventType == eventType);

        public async Task HandleAsync(object @event, EFReadDbContext db, CancellationToken ct)
        {
            var t = @event.GetType();
            foreach (var h in _handlers)
                if (h.EventType == t)
                    await h.ApplyAsync(@event, db, ct);
        }

        // ----------- Internals -----------

        private interface IHandler
        {
            Type EventType { get; }
            Task ApplyAsync(object evt, EFReadDbContext db, CancellationToken ct);
        }

        private sealed class CreateMapHandler<TEvent> : IHandler
        {
            private readonly IMapper _mapper;
            private readonly Func<TEvent, object> _key;
            public Type EventType => typeof(TEvent);

            public CreateMapHandler(IMapper mapper, Func<TEvent, object> key)
            { _mapper = mapper; _key = key; }

            public async Task ApplyAsync(object evt, EFReadDbContext db, CancellationToken ct)
            {
                var e = (TEvent)evt;
                var key = _key(e);
                var set = db.Set<TRead>();

                var existing = await set.FindAsync(new[] { key }, ct);
                if (existing != null) return;

                var doc = _mapper.Map<TRead>(e);
                set.Add(doc);
                await db.SaveChangesAsync(ct);
            }
        }

        private sealed class UpdateMapHandler<TEvent> : IHandler
        {
            private readonly IMapper _mapper;
            private readonly Func<TEvent, object> _key;
            public Type EventType => typeof(TEvent);

            public UpdateMapHandler(IMapper mapper, Func<TEvent, object> key)
            { _mapper = mapper; _key = key; }

            public async Task ApplyAsync(object evt, EFReadDbContext db, CancellationToken ct)
            {
                var e = (TEvent)evt;
                var key = _key(e);
                var set = db.Set<TRead>();

                var doc = await set.FindAsync(new[] { key }, ct);
                if (doc is null) return;

                _mapper.Map(e, doc);
                await db.SaveChangesAsync(ct);
            }
        }

        private sealed class UpdateMapManyHandler<TEvent> : IHandler
        {
            private readonly IMapper _mapper;
            private readonly Func<EFReadDbContext, TEvent, IQueryable<TRead>> _selector;
            public Type EventType => typeof(TEvent);

            public UpdateMapManyHandler(IMapper mapper, Func<EFReadDbContext, TEvent, IQueryable<TRead>> selector)
            { _mapper = mapper; _selector = selector; }

            public async Task ApplyAsync(object evt, EFReadDbContext db, CancellationToken ct)
            {
                var e = (TEvent)evt;
                var docs = await _selector(db, e).ToListAsync(ct);
                if (docs.Count == 0) return;

                foreach (var doc in docs)
                    _mapper.Map(e, doc);

                await db.SaveChangesAsync(ct);
            }
        }

        private sealed class UpdateAsyncHandler<TEvent> : IHandler
        {
            private readonly Func<TEvent, object> _key;
            private readonly Func<TRead, TEvent, EFReadDbContext, CancellationToken, Task> _applyAsync;
            public Type EventType => typeof(TEvent);

            public UpdateAsyncHandler(
                Func<TEvent, object> key,
                Func<TRead, TEvent, EFReadDbContext, CancellationToken, Task> applyAsync)
            { _key = key; _applyAsync = applyAsync; }

            public async Task ApplyAsync(object evt, EFReadDbContext db, CancellationToken ct)
            {
                var e = (TEvent)evt;
                var key = _key(e);
                var set = db.Set<TRead>();

                var doc = await set.FindAsync(new[] { key }, ct);
                if (doc is null) return;

                await _applyAsync(doc, e, db, ct);
                await db.SaveChangesAsync(ct);
            }
        }

        private sealed class UpdateManyAsyncHandler<TEvent> : IHandler
        {
            private readonly Func<EFReadDbContext, TEvent, IQueryable<TRead>> _selector;
            private readonly Func<TRead, TEvent, EFReadDbContext, CancellationToken, Task> _applyAsync;
            public Type EventType => typeof(TEvent);

            public UpdateManyAsyncHandler(
                Func<EFReadDbContext, TEvent, IQueryable<TRead>> selector,
                Func<TRead, TEvent, EFReadDbContext, CancellationToken, Task> applyAsync)
            { _selector = selector; _applyAsync = applyAsync; }

            public async Task ApplyAsync(object evt, EFReadDbContext db, CancellationToken ct)
            {
                var e = (TEvent)evt;
                var docs = await _selector(db, e).ToListAsync(ct);
                if (docs.Count == 0) return;

                foreach (var doc in docs)
                    await _applyAsync(doc, e, db, ct);

                await db.SaveChangesAsync(ct);
            }
        }

        private sealed class DeleteHandler<TEvent> : IHandler
        {
            private readonly Func<TEvent, object> _key;
            public Type EventType => typeof(TEvent);

            public DeleteHandler(Func<TEvent, object> key) => _key = key;

            public async Task ApplyAsync(object evt, EFReadDbContext db, CancellationToken ct)
            {
                var e = (TEvent)evt;
                var key = _key(e);
                var set = db.Set<TRead>();

                var doc = await set.FindAsync(new[] { key }, ct);
                if (doc is null) return;

                set.Remove(doc);
                await db.SaveChangesAsync(ct);
            }
        }
    }
}
