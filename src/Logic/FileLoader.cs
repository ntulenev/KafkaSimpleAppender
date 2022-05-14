using Newtonsoft.Json.Linq;

namespace Logic
{
    public class FileLoader : IFileLoader
    {
        public async Task<IEnumerable<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(filePath);

            var fileContent = await File.ReadAllTextAsync(filePath, ct);
            var jArray = JArray.Parse(fileContent);

            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in jArray)
            {
                var key = item["Key"].ToString();
                var value = item["Value"].ToString();

                result.Add(new KeyValuePair<string, string>(key, value));
            }

            return result;
        }
    }
}
