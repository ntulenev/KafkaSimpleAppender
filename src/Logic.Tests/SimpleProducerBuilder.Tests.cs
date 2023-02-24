using System;
using System.Collections.Generic;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using Xunit;

using Logic.Configuration;

namespace Logic.Tests;

public class SimpleProducerBuilderTests
{

    [Fact(DisplayName = "SimpleProducerBuilder could be created.")]
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
        var exception = Record.Exception(() => new SimpleProducerBuilder(configMock.Object, Mock.Of<ILogger<SimpleProducerBuilder>>()));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = "SimpleProducerBuilder can't be created without logger.")]
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
        var exception = Record.Exception(() => new SimpleProducerBuilder(configMock.Object, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "SimpleProducerBuilder can't be created without config.")]
    [Trait("Category", "Unit")]
    public void CantBeCreatedWithNullConfig()
    {
        // Act
        var exception = Record.Exception(() => new SimpleProducerBuilder(null!, Mock.Of<ILogger<SimpleProducerBuilder>>()));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "SimpleProducerBuilder could be created.")]
    [Trait("Category", "Unit")]
    public void CouldBuildProducer()
    {
        // Arrange
        var config = new BootstrapConfiguration
        {
            BootstrapServers = new List<string> { "test" }
        };

        var configMock = new Mock<IOptions<BootstrapConfiguration>>(MockBehavior.Strict);
        configMock.Setup(x => x.Value).Returns(config);

        var builder = new SimpleProducerBuilder(configMock.Object, Mock.Of<ILogger<SimpleProducerBuilder>>());

        // Act
        var result = builder.Build<string>();

        // Assert
        result.Should().NotBeNull();
    }
}
