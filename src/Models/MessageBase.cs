namespace Models
{
    /// <summary>
    /// Base class for any Kafka message.
    /// </summary>
    /// <typeparam name="TKey">Key marker type.</typeparam>
    public abstract class MessageBase<TKey>
    {
        /// <summary>
        /// Message payload.
        /// </summary>
        public string Payload { get; }

        /// <summary>
        /// Creates Message.
        /// </summary>
        /// <param name="payload">Message payload</param>
        /// <exception cref="ArgumentNullException">Throws if payload is null.</exception>
        /// <exception cref="ArgumentException">Throws if payload is empty.</exception>
        public MessageBase(string payload)
        {
            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload), "Topic name is not set.");
            }

            if (string.IsNullOrEmpty(payload))
            {
                throw new ArgumentException("The topic name cannot be empty or consist of whitespaces.", nameof(payload));
            }

            Payload = payload;
        }
    }
}
