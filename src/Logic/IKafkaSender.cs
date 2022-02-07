using Models;

namespace Logic
{
    public interface IKafkaSender
    {
        public Task SendAsync<TKey>(Topic topic, Message<TKey> message, CancellationToken ct);
    }
}
