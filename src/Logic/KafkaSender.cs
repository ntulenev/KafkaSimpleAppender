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

        public async Task SendAsync<TKey>(Topic topic, MessageBase<TKey> message, CancellationToken ct)
        {
            if (message is Message<TKey> keyMessage)
            {
                using var p = new ProducerBuilder<TKey, string>(_config).Build();
                var dr = await p.ProduceAsync(topic.Name, new Message<TKey, string>
                {
                    Key = keyMessage.Key,
                    Value = keyMessage.Payload
                }
                , ct)
                .ConfigureAwait(false);
            }
            else if (message is NoKeyMessage noKeyMessage)
            {
                using var p = new ProducerBuilder<Null, string>(_config).Build();
                var dr = await p.ProduceAsync(topic.Name, new Message<Null, string>
                {
                    Value = noKeyMessage.Payload
                }
                , ct)
                .ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException($"Not supported message type {message.GetType().FullName}", nameof(message));
            }
        }

        private readonly ProducerConfig _config;
    }
}
