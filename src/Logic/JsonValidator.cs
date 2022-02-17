using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Logic
{
    /// <summary>
    /// Json Validator.
    /// </summary>
    public class JsonValidator : IJsonValidator
    {
        /// <summary>
        /// Creates <see cref="JsonValidator"/>.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <exception cref="ArgumentNullException">Throws if logger is null.</exception>
        public JsonValidator(ILogger<JsonValidator> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thows if value is null.</exception>
        public bool IsValid(string value)
        {
            ArgumentNullException.ThrowIfNull(value);

            try
            {
                var token = JToken.Parse(value);

                var result = token.Type == JTokenType.Object || token.Type == JTokenType.Array;

                if (result is false)
                {
                    _logger.LogWarning("Json {JSON} is valid but not object or array.", value);
                }

                return token.Type == JTokenType.Object || token.Type == JTokenType.Array;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Json {JSON} validation fails.", value);
                return false;
            }
        }

        private readonly ILogger<JsonValidator> _logger;
    }
}
