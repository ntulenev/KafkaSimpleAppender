using Models;

using Microsoft.Extensions.Logging;

namespace Logic;

/// <summary>
/// A Kafka message send handler that sends messages to a Kafka topic.
/// </summary>
public class KafkaSendHandler : IKafkaSendHandler
{
    /// <inheritdoc/>
    public IEnumerable<KeyType> ValidKeyTypes { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaSendHandler"/> class with the specified sender, validator, and logger.
    /// </summary>
    /// <param name="sender">The Kafka message sender.</param>
    /// <param name="validator">The JSON validator.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
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
    /// <exception cref="ArgumentNullException">Thrown when the data parameter is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the message payload or key is not a valid JSON string.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the key type is not a valid <see cref="KeyType"/> value.</exception>
    public async Task HandleAsync(string topicName, KeyType keyType, string key, string payload, bool jsonPayload, CancellationToken ct)
    {
        _logger.LogDebug("Start handle topic {Topic} for type {KeyType}, key {Key}, payload {Payload}, payload is json :{IsPayloadJson}",
                        topicName, keyType, key, payload, jsonPayload);

        await HandleAsync(topicName, keyType, new[] { new KeyValuePair<string, string>(key, payload) }, jsonPayload, delegate { }, ct).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when the data parameter is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the message payload or key is not a valid JSON string.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the key type is not a valid <see cref="KeyType"/> value.</exception>
    public async Task HandleAsync(string topicName, KeyType keyType, IEnumerable<KeyValuePair<string, string>> data, bool jsonPayload, Action<int> progressDelegate, CancellationToken ct)
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
                    await _sender.SendAsync(topic, messages, progressDelegate, ct);
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
                    }).ToList();

                    await _sender.SendAsync(topic, messages, progressDelegate, ct);
                    break;
                }
            case KeyType.Long:
                {
                    var messages = data.Select(x => new Message<long>(long.Parse(x.Key), x.Value)).ToList();
                    await _sender.SendAsync(topic, messages, progressDelegate, ct);
                    break;
                }
            case KeyType.NotSet:
                {
                    var messages = data.Select(x => new NoKeyMessage(x.Value)).ToList();
                    await _sender.SendAsync(topic, messages, progressDelegate, ct);
                    break;
                }
            default: throw new ArgumentOutOfRangeException(nameof(keyType));
        }

    }

    private readonly IKafkaSender _sender;
    private readonly IJsonValidator _validator;
    private readonly ILogger<KafkaSendHandler> _logger;
}
