using Microsoft.Extensions.Options;

using Confluent.Kafka;

using Logic.Configuration;
using Models;

namespace Logic
{
    public class KafkaSender : IKafkaSender
    {
        public KafkaSender(IOptions<BootstrapConfiguration> config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _config = new ProducerConfig { BootstrapServers = string.Join(',', config.Value.BootstrapServers) };
        }

        public async Task SendAsync<TKey>(Topic topic, Message<TKey> message, CancellationToken ct)
        {
            if (message.Key is not null)
            {
                using var p = new ProducerBuilder<TKey, string>(_config).Build();
                var dr = await p.ProduceAsync(topic.Name, new Message<TKey, string>
                {
                    Key = message.Key,
                    Value = message.Value
                }
                , ct)
                .ConfigureAwait(false);
            }
            else
            {
                using var p = new ProducerBuilder<Null, string>(_config).Build();
                var dr = await p.ProduceAsync(topic.Name, new Message<Null, string>
                {
                    Value = message.Value
                }
                , ct)
                .ConfigureAwait(false);
            }
        }

        private readonly ProducerConfig _config;
    }
}
