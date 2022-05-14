using Models;

using Microsoft.Extensions.Logging;

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
        /// <param name="logger">logger.</param>
        /// <exception cref="ArgumentNullException">Throws if sender or validator or logger is null.</exception>
        public KafkaSendHandler(IKafkaSender sender,
                                IJsonValidator validator,
                                ILogger<KafkaSendHandler> logger)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            ValidKeyTypes = Enum.GetValues(typeof(KeyType)).Cast<KeyType>().ToList();

            _logger.LogDebug("Handler created");
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Throws if payload or key is not json.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if key type is not supported.</exception>
        public async Task HandleAsync(string topicName, KeyType keyType, string key, string payload, bool jsonPayload, CancellationToken ct)
        {
            _logger.LogDebug("Start handle topic {Topic} for type {KeyType}, key {Key}, payload {Payload}, payload is json :{IsPayloadJson}",
                            topicName, keyType, key, payload, jsonPayload);

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
                        await _sender.SendAsync(topic, message, ct);
                        break;
                    }
                case KeyType.JSON:
                    {
                        if (!_validator.IsValid(key))
                        {
                            throw new ArgumentException("Message key is not valid json", nameof(key));
                        }

                        var message = new Message<string>(key, payload);
                        await _sender.SendAsync(topic, message, ct);
                        break;
                    }
                case KeyType.Long:
                    {
                        var message = new Message<long>(long.Parse(key), payload);
                        await _sender.SendAsync(topic, message, ct);
                        break;
                    }
                case KeyType.NotSet:
                    {
                        var message = new NoKeyMessage(payload);
                        await _sender.SendAsync(topic, message, ct);
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(keyType));
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">If data is not set.</exception>
        /// <exception cref="ArgumentException">Throws if payload or key is not json.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if key type is not supported.</exception>
        public async Task HandleAsync(string topicName, KeyType keyType, IEnumerable<KeyValuePair<string, string>> data, bool jsonPayload, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(data);

            _logger.LogDebug("Start handle data collection topic {Topic} for type {KeyType}, payload is json :{IsPayloadJson}",
                         topicName, keyType, jsonPayload);

            var topic = new Topic(topicName);

            if (jsonPayload)
            {
                if (data.Select(x => _validator.IsValid(x.Value)).Any(x => x is false))
                {
                    throw new ArgumentException("Message payload is not valid json", nameof(data));
                }
            }

            switch (keyType)
            {
                case KeyType.String:
                    {
                        var messages = data.Select(x => new Message<string>(x.Key, x.Value));
                        await Task.Yield();
                        break;
                    }
                case KeyType.JSON:
                    {
                        var messages = data.Select(x =>
                        {
                            if (!_validator.IsValid(x.Key))
                            {
                                throw new ArgumentException("Message key is not valid json", nameof(data));
                            }

                            return new Message<string>(x.Key, x.Value);
                        });

                        await Task.Yield();
                        break;
                    }
                case KeyType.Long:
                    {
                        var messages = data.Select(x => new Message<long>(long.Parse(x.Key), x.Value));

                        await Task.Yield();

                        break;
                    }
                case KeyType.NotSet:
                    {
                        var messages = data.Select(x => new NoKeyMessage(x.Value));

                        await Task.Yield();

                        //await _sender.SendAsync(topic, message, ct);
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(keyType));
            }

        }

        private readonly IKafkaSender _sender;
        private readonly IJsonValidator _validator;
        private readonly ILogger<KafkaSendHandler> _logger;
    }
}
