using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Confluent.Kafka;

using Logic.Configuration;

namespace Logic
{
    /// <summary>
    /// Simple builder for Kafka producer
    /// </summary>
    public class SimpleProducerBuilder : IProducerBuilder
    {
        /// <summary>
        /// Creates <see cref="SimpleProducerBuilder"/>.
        /// </summary>
        /// <param name="config">Kafka sender configuration.</param>
        /// <param name="logger">Logger.</param>
        /// <exception cref="ArgumentNullException">Thows if config is null.</exception>
        /// /// <exception cref="ArgumentNullException">thows if logger is null.</exception>
        public SimpleProducerBuilder(IOptions<BootstrapConfiguration> config,
                   ILogger<SimpleProducerBuilder> logger)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var configData = config.Value;

            _config = new() { BootstrapServers = configData.CreateConnectionString() };

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogDebug("Builder created");
        }

        /// <inheritdoc/>
        public IProducer<TKey, string> Build<TKey>()
        {
            return new ProducerBuilder<TKey, string>(_config).Build();
        }

        private readonly ProducerConfig _config;
        private readonly ILogger<SimpleProducerBuilder> _logger;
    }
}
