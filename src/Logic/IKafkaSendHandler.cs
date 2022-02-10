using Models;

namespace Logic
{
    public interface IKafkaSendHandler
    {
        public Task HandleAsync(string topicName, KeyType keyType, string key, string payload, bool jsonPayload, CancellationToken ct);

        public IEnumerable<KeyType> ValidKeyTypes { get; }
    }
}
