using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using Confluent.Kafka;

using Logic.Configuration;
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
        public KafkaSender(IOptions<BootstrapConfiguration> config,
                           ILogger<KafkaSender> logger)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var configData = config.Value;

            _config = new ProducerConfig { BootstrapServers = string.Join(',', configData.BootstrapServers) };

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
                    using var p = new ProducerBuilder<TKey, string>(_config).Build();
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
                    using var p = new ProducerBuilder<Null, string>(_config).Build();
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

        private readonly ProducerConfig _config;
        private readonly ILogger<KafkaSender> _logger;
    }
}
