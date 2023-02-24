using Xunit;

using System;

using FluentAssertions;

namespace Models.Tests;

public class NoKeyMessageTests
{
    [Fact(DisplayName = "NoKeyMessage payload can't be null.")]
    [Trait("Category", "Unit")]
    public void CantCreateWithNullPayload()
    {

        // Act
        var exception = Record.Exception(() => new NoKeyMessage(null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "NoKeyMessage payload can't be empty.")]
    [Trait("Category", "Unit")]
    public void CantCreateWithEmptyPayload()
    {
        // Arrange
        var payload = string.Empty;

        // Act
        var exception = Record.Exception(() => new NoKeyMessage(payload));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "NoKeyMessage can be created.")]
    [Trait("Category", "Unit")]
    public void CanCreateWithValidParams()
    {
        // Arrange
        var payload = "test";

        // Act
        var exception = Record.Exception(() => new NoKeyMessage(payload));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = "NoKeyMessage contains payload.")]
    [Trait("Category", "Unit")]
    public void MessageContainsPayload()
    {
        // Arrange
        var payload = "test";

        // Act
        var message = new NoKeyMessage(payload);

        // Assert
        message.Payload.Should().Be(payload);
    }
}
