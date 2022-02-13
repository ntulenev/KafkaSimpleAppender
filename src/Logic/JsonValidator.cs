using Newtonsoft.Json.Linq;

namespace Logic
{
    /// <summary>
    /// Json Validator.
    /// </summary>
    public class JsonValidator : IJsonValidator
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thows if value is null.</exception>
        public bool IsValid(string value)
        {
            ArgumentNullException.ThrowIfNull(value);

            try
            {
                var token = JToken.Parse(value);
                return token.Type == JTokenType.Object || token.Type == JTokenType.Array;
            }
            catch
            {
                return false;
            }
        }
    }
}
