using System;
using System.Collections.Generic;

using FluentAssertions;

using Xunit;

using Logic.Configuration;

namespace Logic.Tests
{
    public class BootstrapConfigurationTests
    {
        [Fact(DisplayName = "BootstrapConfiguration could be created.")]
        [Trait("Category", "Unit")]
        public void CouldBeCreated()
        {
            // Act
            var exception = Record.Exception(() => new BootstrapConfiguration());

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = "BootstrapConfiguration creates valid config string.")]
        [Trait("Category", "Unit")]
        public void CanCreateConnectionStrnig()
        {
            // Arrange
            var config = new BootstrapConfiguration()
            {
                BootstrapServers = new List<string>
                 {
                     "A","B"
                 }
            };

            // Act
            var result = config.CreateConnectionString();

            // Assert
            result.Should().Be("A,B");
        }

        [Fact(DisplayName = "BootstrapConfiguration cant create connection string from null collection.")]
        [Trait("Category", "Unit")]
        public void CantCreateConnectionStrnig()
        {
            // Arrange
            var config = new BootstrapConfiguration();

            // Act
            var exception = Record.Exception(() => _ = config.CreateConnectionString());

            // Assert
            exception.Should().NotBeNull().And.BeOfType<InvalidOperationException>();
        }

        [Fact(DisplayName = "BootstrapConfiguration creates empty config string.")]
        [Trait("Category", "Unit")]
        public void CanCreateEmptyConnectionStrnig()
        {
            // Arrange
            var config = new BootstrapConfiguration()
            {
                BootstrapServers = new List<string>
                {
                }
            };

            // Act
            var result = config.CreateConnectionString();

            // Assert
            result.Should().Be(string.Empty);
        }
    }
}
