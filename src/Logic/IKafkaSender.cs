using Models;

namespace Logic
{
    public interface IKafkaSender
    {
        public Task SendAsync<TKey>(Topic topic, MessageBase<TKey> message, CancellationToken ct);
    }
}
