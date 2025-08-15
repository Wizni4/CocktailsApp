

namespace CocktailsApp.ReadStore.Projections
{
    public sealed class KafkaOptions
    {
        public string BootstrapServers { get; set; } = default!;
        public string Topic { get; set; } = default!;
        public string GroupId { get; set; } = default!;
    }
}
