using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Confluent.Kafka;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using Models;

namespace Logic.Tests
{
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

        [Fact(DisplayName = "KafkaSender cant send message to null topic.")]
        [Trait("Category", "Unit")]
        public async void CantSendInNullTopic()
        {
            // Arrange
            var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());
            using var cts = new CancellationTokenSource();
            var testMessage = new NoKeyMessage("test");

            // Act
            var exception = await Record.ExceptionAsync(() => sender.SendAsync(null!, testMessage, cts.Token));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "KafkaSender cant send null message.")]
        [Trait("Category", "Unit")]
        public async void CantSendNullMessage()
        {
            // Arrange
            var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());
            using var cts = new CancellationTokenSource();
            var topic = new Topic("test");

            // Act
            var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, (NoKeyMessage)null!, cts.Token));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "KafkaSender cant send unknown message type.")]
        [Trait("Category", "Unit")]
        public async void CantSendUnknownMessage()
        {
            // Arrange
            var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());
            using var cts = new CancellationTokenSource();
            var topic = new Topic("test");
            var msg = new TestMessageType("test");

            // Act
            var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, msg, cts.Token));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }

        [Fact(DisplayName = "KafkaSender can send No key message type.")]
        [Trait("Category", "Unit")]
        public async void CanSendNoKeyMessage()
        {
            // Arrange
            using var cts = new CancellationTokenSource();
            var topic = new Topic("test");
            var msg = new NoKeyMessage("test");

            var producerMock = new Mock<IProducer<Null, string>>(MockBehavior.Strict);
            producerMock.Setup(x => x.ProduceAsync(topic.Name, It.Is<Message<Null, string>>(x => x.Value == msg.Payload), cts.Token))
                        .Returns(() => Task.FromResult(new DeliveryResult<Null, string>()));
            producerMock.Setup(x => x.Dispose());

            var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);
            builderMock.Setup(x => x.Build<Null>()).Returns(producerMock.Object);

            var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());

            // Act
            var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, msg, cts.Token));

            // Assert
            exception.Should().BeNull();
            producerMock.Verify(x => x.ProduceAsync(topic.Name, It.Is<Message<Null, string>>(x => x.Value == msg.Payload), cts.Token), Times.Once);
        }

        [Fact(DisplayName = "KafkaSender can send key message type.")]
        [Trait("Category", "Unit")]
        public async void CanSendKeyMessage()
        {
            // Arrange
            using var cts = new CancellationTokenSource();
            var topic = new Topic("test");
            var msg = new Message<int>(1, "test");

            var producerMock = new Mock<IProducer<int, string>>(MockBehavior.Strict);
            producerMock.Setup(x => x.ProduceAsync(topic.Name, It.Is<Message<int, string>>(x => x.Value == msg.Payload && x.Key == msg.Key), cts.Token))
                        .Returns(() => Task.FromResult(new DeliveryResult<int, string>()));
            producerMock.Setup(x => x.Dispose());

            var builderMock = new Mock<IProducerBuilder>(MockBehavior.Strict);
            builderMock.Setup(x => x.Build<int>()).Returns(producerMock.Object);

            var sender = new KafkaSender(builderMock.Object, Mock.Of<ILogger<KafkaSender>>());

            // Act
            var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, msg, cts.Token));

            // Assert
            exception.Should().BeNull();
            producerMock.Verify(x => x.ProduceAsync(topic.Name, It.Is<Message<int, string>>(x => x.Value == msg.Payload && x.Key == msg.Key), cts.Token), Times.Once);
        }
    }
}
