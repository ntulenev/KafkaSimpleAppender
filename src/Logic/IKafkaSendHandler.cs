using Models;

namespace Logic
{
    /// <summary>
    /// Logic handler.
    /// </summary>
    public interface IKafkaSendHandler
    {
        /// <summary>
        /// Process UI data and sends it to Kafka.
        /// </summary>
        /// <param name="topicName">Kafka topic name.</param>
        /// <param name="keyType">Key type</param>
        /// <param name="key">Key value</param>
        /// <param name="payload">Message payload.</param>
        /// <param name="jsonPayload">Validate message payload as json</param>
        /// <param name="ct">Cancellation token.</param>
        public Task HandleAsync(string topicName, KeyType keyType, string key, string payload, bool jsonPayload, CancellationToken ct);

        /// <summary>
        /// Process UI data and sends it to Kafka.
        /// </summary>
        /// <param name="topicName">Kafka topic name.</param>
        /// <param name="keyType">Key type</param>
        /// <param name="data">Key/value data collection</param>
        /// <param name="jsonPayload">Validate message payload as json</param>
        /// <param name="ct">Cancellation token.</param>
        public Task HandleAsync(string topicName, KeyType keyType, IEnumerable<KeyValuePair<string, string>> data, bool jsonPayload, Action<int> progressDelegate, CancellationToken ct);

        /// <summary>
        /// Valid Key types.
        /// </summary>
        public IEnumerable<KeyType> ValidKeyTypes { get; }
    }
}
