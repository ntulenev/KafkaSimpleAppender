using Logic.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Logic
{
    public class FileLoader : IFileLoader
    {
        public FileLoader(IOptions<FileLoaderConfiguration> config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var configData = config.Value;

            _keyFieldName = configData.FileKeyField;
            _valueFieldName = configData.FileValueField;
        }

        public async Task<IReadOnlyCollection<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(filePath);

            var fileContent = await File.ReadAllTextAsync(filePath, ct);
            var jArray = JArray.Parse(fileContent);

            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in jArray)
            {
                var key = item[_keyFieldName]!.ToString();
                var value = item[_valueFieldName]!.ToString();

                result.Add(new KeyValuePair<string, string>(key, value));
            }

            return result;
        }

        private readonly string _keyFieldName;
        private readonly string _valueFieldName;
    }
}
