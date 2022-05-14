namespace Logic
{
    public class FileLoader : IFileLoader
    {
        public Task<IEnumerable<KeyValuePair<string, string>>> LoadAsync(string filePath, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
