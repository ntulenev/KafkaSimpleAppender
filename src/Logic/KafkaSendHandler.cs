using Models;

namespace Logic
{
    /// <summary>
    /// Logic handler.
    /// </summary>
    public class KafkaSendHandler : IKafkaSendHandler
    {
        public IEnumerable<KeyType> ValidKeyTypes { get; }

        /// <summary>
        /// Creates <see cref="KafkaSendHandler"/>.
        /// </summary>
        /// <param name="sender">Sender contract.</param>
        /// <param name="validator">Json validation contract.</param>
        /// <exception cref="ArgumentNullException">Throws if sender is or validator is null.</exception>
        public KafkaSendHandler(IKafkaSender sender,
                                IJsonValidator validator)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));

            ValidKeyTypes = Enum.GetValues(typeof(KeyType)).Cast<KeyType>().ToList();
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Throws if payload is not json.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if key type is not supported.</exception>
        public async Task HandleAsync(string topicName, KeyType keyType, string key, string payload, bool jsonPayload, CancellationToken ct)
        {
            var topic = new Topic(topicName);

            if (jsonPayload)
            {
                if (!_validator.IsValid(payload))
                {
                    throw new ArgumentException("Message payload is not valid json", nameof(key));
                }
            }

            switch (keyType)
            {
                case KeyType.String:
                    {
                        var message = new Message<string>(key, payload);
                        await _sender.SendAsync(topic, message, CancellationToken.None);
                        break;
                    }
                case KeyType.JSON:
                    {
                        if (!_validator.IsValid(key))
                        {
                            throw new ArgumentException("Message key is not valid json", nameof(key));
                        }

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
        private readonly IJsonValidator _validator;
    }
}
