using Models;

namespace Logic
{
    public interface IKafkaSendHandler
    {
        public Task HandleAsync(string topicName, object keyType, string key, string payload, CancellationToken ct);

        public IEnumerable<KeyType> ValidKeyTypes { get; }
    }
}
