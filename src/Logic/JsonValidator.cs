using Microsoft.Extensions.Logging;

using Newtonsoft.Json.Linq;

namespace Logic;

/// <summary>
/// Provides functionality to validate a JSON string.
/// </summary>
public class JsonValidator : IJsonValidator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonValidator"/> class.
    /// </summary>
    /// <param name="logger">The logger to use for logging.</param>
    /// <exception cref="ArgumentNullException">Thrown when the logger is null.</exception>
    public JsonValidator(ILogger<JsonValidator> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
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
