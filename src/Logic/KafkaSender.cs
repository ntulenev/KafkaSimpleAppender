﻿using Microsoft.Extensions.Options;

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

        public async Task SendAsync(Topic topic, Message message, CancellationToken ct)
        {
            using var p = new ProducerBuilder<string, string>(_config).Build();
            var dr = await p.ProduceAsync(topic.Name, new Message<string, string>
            {
                Key = message.Key,
                Value = message.Value
            }
            , ct)
            .ConfigureAwait(false);
        }

        private readonly ProducerConfig _config;
    }
}