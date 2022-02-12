using Xunit;

using System;

using FluentAssertions;

namespace Models.Tests
{
    public class MessageTests
    {
        [Fact(DisplayName = "Message payload can't be null.")]
        [Trait("Category", "Unit")]
        public void CantCreateWithNullPayload()
        {
            // Arrange
            var key = new object();

            // Act
            var exception = Record.Exception(() => new Message<object>(key, null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Message key can't be null.")]
        [Trait("Category", "Unit")]
        public void CantCreateWithNullKey()
        {
            // Arrange
            var payload = "test";

            // Act
            var exception = Record.Exception(() => new Message<object>(null!, payload));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Message payload can't be empty.")]
        [Trait("Category", "Unit")]
        public void CantCreateWithEmptyPayload()
        {
            // Arrange
            var key = new object();
            var payload = string.Empty;

            // Act
            var exception = Record.Exception(() => new Message<object>(key, payload));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }

        [Fact(DisplayName = "Message can be created.")]
        [Trait("Category", "Unit")]
        public void CanCreateWithValidParams()
        {
            // Arrange
            var key = new object();
            var payload = "test";

            // Act
            var exception = Record.Exception(() => new Message<object>(key, payload));

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = "Message contains payload and key.")]
        [Trait("Category", "Unit")]
        public void MessageContainsPayload()
        {
            // Arrange
            var key = new object();
            var payload = "test";

            // Act
            var message = new Message<object>(key, payload);

            // Assert
            message.Payload.Should().Be(payload);
            message.Key.Should().Be(key);
        }
    }
}
