using Confluent.Kafka;

namespace Logic;

/// <summary>
/// Abstraction for ProducerBuilder.
/// </summary>
public interface IProducerBuilder
{
    /// <summary>
    /// Builds Kafka producer.
    /// </summary>
    /// <typeparam name="TKey">Producer key type.</typeparam>
    /// <returns>Kafka producer.</returns>
    public IProducer<TKey, string> Build<TKey>();
}
