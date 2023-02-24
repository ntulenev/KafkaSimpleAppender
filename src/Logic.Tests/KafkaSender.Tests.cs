using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using FluentAssertions;

using Confluent.Kafka;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using Models;

namespace Logic.Tests;

public class KafkaSenderTests
{
    [Fact(DisplayName = "KafkaSender could be created.")]
    [Trait("Category", "Unit")]
    public void CouldBeCreated()
    {
        // Arrange
        var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

        // Act
        var exception = Record.Exception(() => new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>()));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = "KafkaSender can't be created without logger.")]
    [Trait("Category", "Unit")]
    public void CantBeCreatedWithNullLogger()
    {
        // Arrange
        var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

        // Act
        var exception = Record.Exception(() => new KafkaSender(builderMock.Object, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "KafkaSender can't be created without builder.")]
    [Trait("Category", "Unit")]
    public void CantBeCreatedWithNullBuilder()
    {
        // Act
        var exception = Record.Exception(() => new KafkaSender(null!, Mock.Of<ILogger<KafkaSender>>()));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
    
    [Fact(DisplayName = "KafkaSender cant send message collection to null topic.")]
    [Trait("Category", "Unit")]
    public async void CantSendCollectionInNullTopic()
    {
        // Arrange
        var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

        var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());
        using var cts = new CancellationTokenSource();
        var testMessage = new NoKeyMessage("test");

        // Act
        var exception = await Record.ExceptionAsync(() => sender.SendAsync(null!, new[] { testMessage }, _ => { }, cts.Token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "KafkaSender cant send null message collection.")]
    [Trait("Category", "Unit")]
    public async void CantSendNullCollectionMessage()
    {
        // Arrange
        var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

        var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());
        using var cts = new CancellationTokenSource();
        var topic = new Topic("test");

        // Act
        var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, (IEnumerable<NoKeyMessage>)null!, _ => { }, cts.Token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "KafkaSender cant send unknown message type from collection.")]
    [Trait("Category", "Unit")]
    public async void CantSendUnknownMessageFromCollection()
    {
        // Arrange
        var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

        var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());
        using var cts = new CancellationTokenSource();
        var topic = new Topic("test");
        var msg = new TestMessageType("test");

        // Act
        var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, new[] { msg }, _ => { }, cts.Token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "KafkaSender can send No key message collection.")]
    [Trait("Category", "Unit")]
    public async void CanSendNoKeyMessageCollection()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        var topic = new Topic("test");
        var msg1 = new NoKeyMessage("test1");
        var msg2 = new NoKeyMessage("test2");

        var producerMock = new Mock<IProducer<Null, string>>(MockBehavior.Strict);
        producerMock.Setup(x => x.ProduceAsync(topic.Name, It.Is<Message<Null, string>>(x => x.Value == msg1.Payload), cts.Token))
                    .Returns(() => Task.FromResult(new DeliveryResult<Null, string>()));
        producerMock.Setup(x => x.ProduceAsync(topic.Name, It.Is<Message<Null, string>>(x => x.Value == msg2.Payload), cts.Token))
        .Returns(() => Task.FromResult(new DeliveryResult<Null, string>()));
        producerMock.Setup(x => x.Dispose());

        var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);
        builderMock.Setup(x => x.Build<Null>()).Returns(producerMock.Object);

        var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());

        var counter = 0;

        // Act
        var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, new[] { msg1, msg2 }, i => counter = i, cts.Token));

        // Assert
        exception.Should().BeNull();
        counter.Should().Be(2);
        producerMock.Verify(x => x.ProduceAsync(topic.Name, It.Is<Message<Null, string>>(x => x.Value == msg1.Payload), cts.Token), Times.Once);
        producerMock.Verify(x => x.ProduceAsync(topic.Name, It.Is<Message<Null, string>>(x => x.Value == msg2.Payload), cts.Token), Times.Once);
    }

    [Fact(DisplayName = "KafkaSender can send key message collection.")]
    [Trait("Category", "Unit")]
    public async void CanSendKeyMessageCollection()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        var topic = new Topic("test");
        var msg1 = new Message<int>(1, "test1");
        var msg2 = new Message<int>(2, "test2");

        var producerMock = new Mock<IProducer<int, string>>(MockBehavior.Strict);
        producerMock.Setup(x => x.ProduceAsync(topic.Name, It.Is<Message<int, string>>(x => x.Value == msg1.Payload), cts.Token))
                    .Returns(() => Task.FromResult(new DeliveryResult<int, string>()));
        producerMock.Setup(x => x.ProduceAsync(topic.Name, It.Is<Message<int, string>>(x => x.Value == msg2.Payload), cts.Token))
        .Returns(() => Task.FromResult(new DeliveryResult<int, string>()));
        producerMock.Setup(x => x.Dispose());

        var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);
        builderMock.Setup(x => x.Build<int>()).Returns(producerMock.Object);

        var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());

        var counter = 0;

        // Act
        var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, new[] { msg1, msg2 }, i => counter = i, cts.Token));

        // Assert
        exception.Should().BeNull();
        counter.Should().Be(2);
        producerMock.Verify(x => x.ProduceAsync(topic.Name, It.Is<Message<int, string>>(x => x.Value == msg1.Payload), cts.Token), Times.Once);
        producerMock.Verify(x => x.ProduceAsync(topic.Name, It.Is<Message<int, string>>(x => x.Value == msg2.Payload), cts.Token), Times.Once);
    }
}
