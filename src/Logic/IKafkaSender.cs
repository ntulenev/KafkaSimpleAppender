using Models;

namespace Logic
{
    /// <summary>
    /// Kafka sender.
    /// </summary>
    public interface IKafkaSender
    {
        /// <summary>
        /// Sends message to Kafka topic.
        /// </summary>
        /// <typeparam name="TKey">Message Key type.</typeparam>
        /// <param name="topic">Kafka topic.</param>
        /// <param name="message">Kafka message.</param>
        /// <param name="ct">Cancellation token.</param>
        public Task SendAsync<TKey>(Topic topic, MessageBase<TKey> message, CancellationToken ct);

        /// <summary>
        /// Sends messages to Kafka topic.
        /// </summary>
        /// <typeparam name="TKey">Message Key type.</typeparam>
        /// <param name="topic">Kafka topic.</param>
        /// <param name="message">Kafka messages.</param>
        /// <param name="ct">Cancellation token.</param>
        public Task SendAsync<TKey>(Topic topic, IEnumerable<MessageBase<TKey>> messages, Action<int> progressDelegate, CancellationToken ct);
    }
}
