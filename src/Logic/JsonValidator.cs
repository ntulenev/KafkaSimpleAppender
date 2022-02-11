using System.Text.Json;

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
                _ = JsonDocument.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
