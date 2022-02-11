namespace Models
{
    /// <summary>
    /// Creates Message with Key.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    public class Message<TKey> : MessageBase<TKey>
    {
        /// <summary>
        /// Message key.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Creates <see cref="Message{TKey}"/>.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="payload">Message payload.</param>
        /// <exception cref="ArgumentNullException">Thows if key is null.</exception>
        /// <exception cref="ArgumentNullException">Thows if payload is null.</exception>
        /// <exception cref="ArgumentException">Throws if payload is empty.</exception>
        public Message(TKey key, string payload) : base(payload)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }
    }
}
