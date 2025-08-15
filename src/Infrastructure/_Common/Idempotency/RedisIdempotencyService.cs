using CocktailsApp.Application.Common;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using StackExchange.Redis;


namespace CocktailsApp.Infrastructure.Common
{
    public sealed class RedisIdempotencyService(
        IConnectionMultiplexer mux,
        string @namespace,
        IOptions<IdempotencyOptions> options,
        JsonSerializerSettings? json = null
     ) : IIdempotencyService, IDisposable
    {
        private readonly IConnectionMultiplexer _mux = mux;
        private readonly IDatabase _db = mux.GetDatabase();
        private readonly string _nsData = @namespace + "data:";
        private readonly string _nsLock = @namespace + "lock:";
        private readonly IOptions<IdempotencyOptions> _options = options;
        private readonly JsonSerializerSettings _json = json ?? new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            Formatting = Formatting.None
        };

        private string KD(string key) => _nsData + key;
        private string KL(string key) => _nsLock + key;

        public async Task<CacheHit<T>> TryGetAsync<T>(string key, CancellationToken ct = default)
        {
            var s = await _db.StringGetAsync(KD(key));
            if (s.IsNullOrEmpty) return CacheHit<T>.Miss;

            var obj = JsonConvert.DeserializeObject<T>(s!);
            return obj is null ? CacheHit<T>.Miss : CacheHit<T>.Hit(obj);
        }

        public async Task SaveAsync<T>(string key, T value, CancellationToken ct = default)
        {
            var payload = JsonConvert.SerializeObject(value, _json);

            // Store result and clear lock in one go (not strictly atomic across both keys,
            // but OK for most cases; use Lua script if you need atomicity)
            var batch = _db.CreateBatch();
            var setTask = batch.StringSetAsync(KD(key), payload, TimeSpan.FromSeconds(_options.Value.LockTtlSeconds));
            var delTask = batch.KeyDeleteAsync(KL(key));
            batch.Execute();

            await Task.WhenAll(setTask, delTask);
        }

        public void Dispose() => _mux.Dispose();
    }
}
