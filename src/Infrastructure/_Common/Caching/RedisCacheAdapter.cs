
using CocktailsApp.Application.Common;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using StackExchange.Redis;



namespace CocktailsApp.Infrastructure.Common
{
    public sealed class RedisCacheAdapter(
        IConnectionMultiplexer mux,
        string @namespace,
        JsonSerializerSettings? json = null
    ) : ICacheService, IDisposable
    {
        private readonly IConnectionMultiplexer _mux = mux;
        private readonly IDatabase _db = mux.GetDatabase();
        private readonly string _namespace = @namespace;
        private readonly JsonSerializerSettings _json = json ?? new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            Formatting = Formatting.None
        };
        private string K(string key) => _namespace + key;

        public async Task<CacheHit<T>> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            var val = await _db.StringGetAsync(K(key));
            if (val.IsNullOrEmpty) return CacheHit<T>.Miss;

            var obj = JsonConvert.DeserializeObject<T>(val!, _json);
            return obj is null ? CacheHit<T>.Miss : CacheHit<T>.Hit(obj);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken cancellationToken)
        {
            var payload = JsonConvert.SerializeObject(value, _json);
            return _db.StringSetAsync(K(key), payload, ttl);
        }

        public void Dispose() => _mux.Dispose();
    }
}
