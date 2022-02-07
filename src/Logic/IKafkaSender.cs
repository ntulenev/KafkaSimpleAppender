using Models;

namespace Logic
{
    public interface IKafkaSender
    {
        public Task SendAsync(Message message, CancellationToken ct);
    }
}
