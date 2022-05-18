using System;

using FluentAssertions;

using Logic.Configuration;

using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace Logic.Tests
{
    public class FileLoaderTests
    {
        [Fact(DisplayName = "FileLoader can't be created with null config.")]
        [Trait("Category", "Unit")]
        public void CantBeCreatedWithNullConfig()
        {
            // Act
            var exception = Record.Exception(() => new FileLoader(null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "FileLoader could be created.")]
        [Trait("Category", "Unit")]
        public void CouldBeCreated()
        {
            // Arrange
            var conf = new FileLoaderConfiguration()
            {
                FileKeyField = "Key",
                FileValueField = "Value"
            };
            var configMock = new Mock<IOptions<FileLoaderConfiguration>>(MockBehavior.Strict);
            configMock.Setup(x => x.Value).Returns(conf);

            // Act
            var exception = Record.Exception(() => new FileLoader(configMock.Object));

            // Assert
            exception.Should().BeNull();
        }
    }
}
