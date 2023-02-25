namespace Logic;

/// <summary>
/// Interface for loading files as a key-value pairs collection.
/// </summary>
public interface IFileLoader
{
    /// <summary>
    /// Loads a file asynchronously and returns a collection of key-value pairs.
    /// </summary>
    /// <param name="filePath">The path to the file to load.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is a read-only collection 
    /// of key-value pairs from the content of the file.</returns>
    public Task<IReadOnlyCollection<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct);
}
