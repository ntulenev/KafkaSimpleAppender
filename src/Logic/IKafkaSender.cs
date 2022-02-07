using Models;

namespace Logic
{
    public interface IKafkaSender
    {
        public Task SendAsync(Topic topic, Message message, CancellationToken ct);
    }
}
