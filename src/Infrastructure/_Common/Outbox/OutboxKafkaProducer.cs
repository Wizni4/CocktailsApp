using Confluent.Kafka;

using Microsoft.Extensions.Options;

using System.Text;

namespace CocktailsApp.Infrastructure.Common
{
    public sealed class OutboxKafkaProducer(
        IOptions<KafkaOptions> options
    ) : IKafkaProducer, IDisposable
    {
        private readonly IProducer<string, string> _producer = new ProducerBuilder<string, string>(new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            Acks = Acks.All,
            EnableIdempotence = true,
        }).Build();

        public async Task ProduceAsync(
            string key,
            string value,
            IEnumerable<KeyValuePair<string, string>>? headers = null,
            CancellationToken ct = default)
        {
            var msg = new Message<string, string> { Key = key, Value = value };
            if (headers != null)
            {
                msg.Headers = new Headers();
                foreach (var h in headers)
                    msg.Headers.Add(h.Key, Encoding.UTF8.GetBytes(h.Value));
            }

            // Throws on error -> caller decides retry/mark attempts
            await _producer.ProduceAsync(options.Value.Topic, msg, ct);
        }

        public void Dispose() => _producer.Flush(TimeSpan.FromSeconds(5));
    }
}
