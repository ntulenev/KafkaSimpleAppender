namespace Logic
{
    /// <summary>
    /// Loader for file data.
    /// </summary>
    public interface IFileLoader
    {
        /// <summary>
        /// Loads message collection from file.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Key/Value messages collection</returns>
        public Task<IReadOnlyCollection<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct);
    }
}
