
namespace CocktailsApp.ReadStore.Common
{
    public sealed class ProcessedEvent
    {
        public Guid EventId { get; set; }
        public string Topic { get; set; } = default!;
        public int Partition { get; set; }
        public long Offset { get; set; }
        public DateTime ProcessedOnUtc { get; set; }
    }
}
