using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Confluent.Kafka;

using Logic.Configuration;

namespace Logic
{
    internal class SimpleProducerBuilder : IProducerBuilder
    {
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

        public IProducer<TKey, string> Build<TKey>()
        {
            return new ProducerBuilder<TKey, string>(_config).Build();
        }

        private readonly ProducerConfig _config;
        private readonly ILogger<SimpleProducerBuilder> _logger;
    }
}
