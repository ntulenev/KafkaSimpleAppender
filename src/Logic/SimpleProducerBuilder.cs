using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Confluent.Kafka;

using Logic.Configuration;

namespace Logic;

/// <summary>
/// Represents a simple producer builder that implements the <see cref="IProducerBuilder"/> interface.
/// </summary>
public class SimpleProducerBuilder : IProducerBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleProducerBuilder"/> class with the 
    /// specified bootstrap configuration.
    /// </summary>
    /// <param name="config">The bootstrap configuration.</param>
    /// <param name="logger">The logger to be used for logging.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> or <paramref name="logger"/> is null.</exception>
    public SimpleProducerBuilder(
               IOptions<BootstrapConfiguration> config,
               ILogger<SimpleProducerBuilder> logger)
    {
        ArgumentNullException.ThrowIfNull(config);

        var configData = config.Value;

        _config = new()
        {
            BootstrapServers = configData.CreateConnectionString(),
            SecurityProtocol = configData.SecurityProtocol,
            SaslMechanism = configData.SASLMechanism,
            SaslUsername = configData.Username,
            SaslPassword = configData.Password,
            MessageMaxBytes = configData.MessageMaxBytes
        };

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
