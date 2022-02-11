namespace Models
{
    /// <summary>
    /// Kafka message with not set Key
    /// </summary>
    public class NoKeyMessage : MessageBase<object>
    {
        /// <summary>
        /// Creates <see cref="NoKeyMessage"/>
        /// </summary>
        /// <param name="payload">Mesasge text.</param>
        /// <exception cref="ArgumentNullException">Thows if payload is null.</exception>
        /// <exception cref="ArgumentException">Throws if payload is empty.</exception>
        public NoKeyMessage(string payload) : base(payload)
        {

        }
    }
}
