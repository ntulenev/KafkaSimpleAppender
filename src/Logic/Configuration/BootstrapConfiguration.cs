using Confluent.Kafka;

namespace Logic.Configuration
{
    /// <summary>
    /// Bootstrap servers config.
    /// </summary>
    public class BootstrapConfiguration
    {
        /// <summary>
        /// List of bootstrap servers
        /// </summary>
        public List<string> BootstrapServers { get; set; } = default!;

        /// <summary>
        /// Kafka user name, is any.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Kafka password name, is any.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Kafka security protocol.
        /// </summary>
        public SecurityProtocol SecurityProtocol { get; set; } = SecurityProtocol.Plaintext;

        /// <summary>
        /// Kafka security protocol mechanism.
        /// </summary>
        public SaslMechanism? SASLMechanism { get; set; }

        /// <summary>
        /// Maximum Kafka protocol request message size.
        /// </summary>
        public int? MessageMaxBytes { get; set; }

        /// <summary>
        /// Creates Kafka connection string.
        /// </summary>
        /// <returns>Connection string</returns>
        public string CreateConnectionString()
        {
            if (BootstrapServers == null)
            {
                throw new InvalidOperationException("BootstrapServers collection is null");
            }

            return string.Join(',', BootstrapServers);
        }
    }
}
