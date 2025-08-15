
namespace CocktailsApp.Infrastructure.Common
{
    public interface IKafkaProducer
    {
        Task ProduceAsync(
            string key,
            string value,
            IEnumerable<KeyValuePair<string, string>>? headers = null,
            CancellationToken ct = default);
    }
}
