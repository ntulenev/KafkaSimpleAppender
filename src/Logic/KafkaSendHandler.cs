using Models;

namespace Logic
{
    public class KafkaSendHandler : IKafkaSendHandler
    {
        public IEnumerable<KeyType> ValidKeyTypes { get; }

        public KafkaSendHandler(IKafkaSender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));

            ValidKeyTypes = Enum.GetValues(typeof(KeyType)).Cast<KeyType>().ToList();
        }

        public async Task HandleAsync(string topicName, KeyType keyType, string key, string payload, CancellationToken ct)
        {
            var topic = new Topic(topicName);

            switch (keyType)
            {
                case KeyType.String:
                    {
                        var message = new Message<string>(key, payload);
                        await _sender.SendAsync(topic, message, CancellationToken.None);
                        break;
                    }
                case KeyType.Long:
                    {
                        var message = new Message<long>(long.Parse(key), payload);
                        await _sender.SendAsync(topic, message, CancellationToken.None);
                        break;
                    }
                case KeyType.NotSet:
                    {
                        var message = new NoKeyMessage(payload);
                        await _sender.SendAsync(topic, message, CancellationToken.None);
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(keyType));
            }
        }

        private readonly IKafkaSender _sender;
    }
}
