using Xunit;

using FluentAssertions;

using Logic.Configuration;

namespace Logic.Tests;

public class FileLoaderConfigurationTests
{
    [Fact(DisplayName = " FileLoaderConfiguration could be created.")]
    [Trait("Category", "Unit")]
    public void CouldBeCreated()
    {
        // Act
        var exception = Record.Exception(() => new FileLoaderConfiguration());

        // Assert
        exception.Should().BeNull();
    }
}
