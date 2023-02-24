using System;

using Models;

namespace Logic.Tests;

/// <summary>
/// Kafka message for test
/// </summary>
public class TestMessageType : MessageBase<object>
{
    /// <summary>
    /// Creates <see cref="TestMessageType"/>
    /// </summary>
    /// <param name="payload">Mesasge text.</param>
    /// <exception cref="ArgumentNullException">Thows if payload is null.</exception>
    /// <exception cref="ArgumentException">Throws if payload is empty.</exception>
    public TestMessageType(string payload) : base(payload)
    {

    }
}
