using System;
using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using Logic.Configuration;
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
            var config = new BootstrapConfiguration
            {
                BootstrapServers = new List<string> { "test" }
            };

            var configMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            // Act
            var exception = Record.Exception(() => new KafkaSender(configMock.Object, Mock.Of<ILogger<KafkaSender>>()));

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = "KafkaSender can't be created without logger.")]
        [Trait("Category", "Unit")]
        public void CantBeCreatedWithNullLogger()
        {
            // Arrange
            var config = new BootstrapConfiguration
            {
                BootstrapServers = new List<string> { "test" }
            };

            var configMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            // Act
            var exception = Record.Exception(() => new KafkaSender(configMock.Object, null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "KafkaSender can't be created without config.")]
        [Trait("Category", "Unit")]
        public void CantBeCreatedWithNullConfig()
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
            var config = new BootstrapConfiguration
            {
                BootstrapServers = new List<string> { "test" }
            };

            var configMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            var sender = new KafkaSender(configMock.Object, Mock.Of<ILogger<KafkaSender>>());
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
            var config = new BootstrapConfiguration
            {
                BootstrapServers = new List<string> { "test" }
            };

            var configMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            var sender = new KafkaSender(configMock.Object, Mock.Of<ILogger<KafkaSender>>());
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
            var config = new BootstrapConfiguration
            {
                BootstrapServers = new List<string> { "test" }
            };

            var configMock = new Mock<IProducerBuilder>(MockBehavior.Strict);

            var sender = new KafkaSender(configMock.Object, Mock.Of<ILogger<KafkaSender>>());
            using var cts = new CancellationTokenSource();
            var topic = new Topic("test");
            var msg = new TestMessageType("test");

            // Act
            var exception = await Record.ExceptionAsync(() => sender.SendAsync(topic, msg, cts.Token));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }
    }
}
