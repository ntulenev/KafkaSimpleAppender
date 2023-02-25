using Logic.Configuration;

using Microsoft.Extensions.Options;

using Newtonsoft.Json.Linq;

namespace Logic;

/// <summary>
/// Loader for kafka messages that store in file.
/// </summary>
public class FileLoader : IFileLoader
{
    /// <summary>
    /// Initializes a new instance of the FileLoader class with the specified configuration options.
    /// </summary>
    /// <param name="config">The configuration options for the FileLoader.</param>
    /// <exception cref="ArgumentNullException">Thrown when the config parameter is null.</exception>
    public FileLoader(IOptions<FileLoaderConfiguration> config)
    {
        ArgumentNullException.ThrowIfNull(config);

        var configData = config.Value;

        _keyFieldName = configData.FileKeyField;
        _valueFieldName = configData.FileValueField;
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when the file is null.</exception>
    public async Task<IReadOnlyCollection<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(filePath);

        var fileContent = await File.ReadAllTextAsync(filePath, ct);
        var jArray = JArray.Parse(fileContent);

        var result = new List<KeyValuePair<string, string>>();
        foreach (var item in jArray)
        {
            var key = item[_keyFieldName]?.ToString() ?? null!;
            var value = item[_valueFieldName]!.ToString();

            result.Add(new KeyValuePair<string, string>(key, value));
        }

        return result;
    }

    private readonly string _keyFieldName;
    private readonly string _valueFieldName;
}
