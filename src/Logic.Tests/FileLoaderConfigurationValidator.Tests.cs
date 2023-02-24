using FluentAssertions;

using Logic.Configuration;
using Logic.Configuration.Validation;

using Microsoft.Extensions.Options;

using Xunit;

namespace Logic.Tests;

public class FileLoaderConfigurationValidatorTests
{
    [Fact(DisplayName = "FileLoaderConfigurationValidator can be created.")]
    [Trait("Category", "Unit")]
    public void CanCreateBootstrapConfigurationValidator()
    {
        // Act
        var exception = Record.Exception(() => new FileLoaderConfigurationValidator());

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = "BootstrapConfigurationValidator success on valid params.")]
    [Trait("Category", "Unit")]
    public void CanSuccessValidate()
    {

        // Arrange
        var validator = new FileLoaderConfigurationValidator();
        var optinos = new FileLoaderConfiguration()
        {
            FileKeyField = "Key",
            FileValueField = "Value"
        };

        // Act
        ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

        // Assert
        result.Should().Be(ValidateOptionsResult.Success);
    }

    [Theory(DisplayName = "BootstrapConfigurationValidator fails on bad key")]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void FailsOnBadKey(string key)
    {

        // Arrange
        var validator = new FileLoaderConfigurationValidator();
        var optinos = new FileLoaderConfiguration()
        {
            FileKeyField = key,
            FileValueField = "Value"
        };

        // Act
        ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

        // Assert
        result.Should().NotBe(ValidateOptionsResult.Success);
        result.Failed.Should().BeTrue();
    }

    [Theory(DisplayName = "BootstrapConfigurationValidator fails on bad value")]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void FailsOnBadValue(string value)
    {

        // Arrange
        var validator = new FileLoaderConfigurationValidator();
        var optinos = new FileLoaderConfiguration()
        {
            FileKeyField = "Key",
            FileValueField = value
        };

        // Act
        ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

        // Assert
        result.Should().NotBe(ValidateOptionsResult.Success);
        result.Failed.Should().BeTrue();
    }

}
