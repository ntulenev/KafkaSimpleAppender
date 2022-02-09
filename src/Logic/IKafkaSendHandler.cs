using Models;

namespace Logic
{
    public interface IKafkaSendHandler
    {
        public Task HandleAsync(string topicName, KeyType keyType, string key, string payload, CancellationToken ct);

        public IEnumerable<KeyType> ValidKeyTypes { get; }
    }
}
