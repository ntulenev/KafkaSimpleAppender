using System.Collections.Generic;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using Xunit;

using Logic.Configuration;
using System;

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

            var configMock = new Mock<IOptions<BootstrapConfiguration>>(MockBehavior.Strict);
            configMock.Setup(x => x.Value).Returns(config);

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

            var configMock = new Mock<IOptions<BootstrapConfiguration>>(MockBehavior.Strict);
            configMock.Setup(x => x.Value).Returns(config);

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
    }
}
