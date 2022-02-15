using Confluent.Kafka;

namespace Logic
{
    public interface IProducerBuilder
    {
        public IProducer<TKey, string> Build<TKey>();
    }
}
