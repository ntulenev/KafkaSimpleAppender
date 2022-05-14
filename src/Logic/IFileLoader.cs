namespace Logic
{
    public interface IFileLoader
    {
        public Task<IReadOnlyCollection<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct);
    }
}
