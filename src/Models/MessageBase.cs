namespace Models;

/// <summary>
/// Represents the base class for a message with a specific payload.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
public abstract class MessageBase<TKey>
{
    /// <summary>
    /// Gets the payload of the message.
    /// </summary>
    public string Payload { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageBase{TKey}"/> class with the specified payload.
    /// </summary>
    /// <param name="payload">The payload of the message.</param>
    /// <exception cref="ArgumentNullException"><paramref name="payload"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="payload"/> is empty or consists of whitespaces.</exception>
    public MessageBase(string payload)
    {
        ArgumentNullException.ThrowIfNull(payload);
        ArgumentNullException.ThrowIfNullOrEmpty(payload);

        Payload = payload;
    }
}
