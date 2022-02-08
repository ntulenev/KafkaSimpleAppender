namespace Models
{
    public abstract class MessageBase<TKey>
    {
        public string Payload { get; }

        public MessageBase(string payload)
        {
            Payload = payload;
        }
    }
}
