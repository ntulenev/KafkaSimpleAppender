namespace Models
{
    public abstract class MessageBase<TKey>
    {
        public string Payload { get; }

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
