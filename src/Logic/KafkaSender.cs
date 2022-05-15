using Microsoft.Extensions.Logging;

using Confluent.Kafka;

using Models;

namespace Logic
{
    /// <summary>
    /// Kafka message sender.
    /// </summary>
    public class KafkaSender : IKafkaSender
    {
        /// <summary>
        /// Creates <see cref="KafkaSender"/>.
        /// </summary>
        /// <param name="config">Kafka sender configuration.</param>
        /// <exception cref="ArgumentNullException">If config is null.</exception>
        public KafkaSender(IProducerBuilder builder,
                           ILogger<KafkaSender> logger)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogDebug("Sender created");
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Throws if message type is not supported.</exception>
        public async Task SendAsync<TKey>(Topic topic, MessageBase<TKey> message, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(topic);
            ArgumentNullException.ThrowIfNull(message);

            if (message is Message<TKey> keyMessage)
            {
                _logger.LogInformation("Sending message {@Message} to {@Topic}", message, topic);

                try
                {
                    using var p = _builder.Build<TKey>();
                    var dr = await p.ProduceAsync(topic.Name, new Message<TKey, string>
                    {
                        Key = keyMessage.Key,
                        Value = keyMessage.Payload
                    }
                    , ct)
                    .ConfigureAwait(false);

                    _logger.LogInformation("Sending done");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error on sending message");
                    throw;
                }

            }
            else if (message is NoKeyMessage noKeyMessage)
            {
                _logger.LogInformation("Sending message {@Message} to {@Topic}", message, topic);

                try
                {
                    using var p = _builder.Build<Null>();
                    var dr = await p.ProduceAsync(topic.Name, new Message<Null, string>
                    {
                        Value = noKeyMessage.Payload
                    }
                    , ct)
                    .ConfigureAwait(false);

                    _logger.LogInformation("Sending done");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error on sending message");
                    throw;
                }
            }
            else
            {
                _logger.LogError("Not supported message type {Type}", message.GetType().FullName);

                throw new ArgumentException($"Not supported message type {message.GetType().FullName}", nameof(message));
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Throws if message type is not supported.</exception>
        public async Task SendAsync<TKey>(Topic topic, IEnumerable<MessageBase<TKey>> messages, Action<int> progressDelegate, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(topic);
            ArgumentNullException.ThrowIfNull(messages);

            if (messages is IEnumerable<Message<TKey>> keyMessages)
            {
                //_logger.LogInformation("Sending message {@Message} to {@Topic}", message, topic);

                try
                {
                    using var p = _builder.Build<TKey>();

                    int progressIndex = 0;

                    foreach (var keyMessage in keyMessages)
                    {
                        var dr = await p.ProduceAsync(topic.Name, new Message<TKey, string>
                        {
                            Key = keyMessage.Key,
                            Value = keyMessage.Payload
                        }
                        , ct)
                        .ConfigureAwait(false);

                        progressDelegate(++progressIndex);

                        _logger.LogInformation("Sending done");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error on sending message");
                    throw;
                }

            }
            else if (messages is IEnumerable<NoKeyMessage> noKeyMessages)
            {
                //_logger.LogInformation("Sending message {@Message} to {@Topic}", message, topic);

                try
                {
                    using var p = _builder.Build<Null>();
                    foreach (var noKeyMessage in noKeyMessages)
                    {
                        var dr = await p.ProduceAsync(topic.Name, new Message<Null, string>
                        {
                            Value = noKeyMessage.Payload
                        }
                        , ct)
                        .ConfigureAwait(false);

                        _logger.LogInformation("Sending done");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error on sending message");
                    throw;
                }
            }
            else
            {
                _logger.LogError("Not supported message type {Type}", messages.GetType().FullName);

                throw new ArgumentException($"Not supported message type {messages.GetType().FullName}", nameof(messages));
            }
        }

        private readonly IProducerBuilder _builder;
        private readonly ILogger<KafkaSender> _logger;
    }
}
