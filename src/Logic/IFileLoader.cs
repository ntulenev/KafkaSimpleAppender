namespace Logic
{
    public interface IFileLoader
    {
        public Task<IEnumerable<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct);
    }
}
