using System.Text.Json;

namespace Logic
{
    public class JsonValidator : IJsonValidator
    {
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
