using Microsoft.Extensions.Options;

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

            _config = config.Value;
        }

        public Task SendAsync(Message message, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        private readonly BootstrapConfiguration _config;
    }
}
