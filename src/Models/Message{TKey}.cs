namespace Models
{
    public class Message<TKey> : MessageBase<TKey>
    {
        public TKey Key { get; }

        public Message(TKey key, string payload) : base(payload)
        {
            Key = key;
        }
    }
}
