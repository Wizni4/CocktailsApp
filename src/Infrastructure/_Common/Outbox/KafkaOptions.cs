

namespace CocktailsApp.Infrastructure.Common
{
    public sealed class KafkaOptions
    {
        public string BootstrapServers { get; set; } = default!;
        public string Topic { get; set; } = default!;
    }
}
