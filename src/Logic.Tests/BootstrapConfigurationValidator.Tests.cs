using System.Linq;
using System.Collections.Generic;

using Xunit;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Logic.Configuration;
using Logic.Configuration.Validation;

namespace Logic
{
    public class BootstrapConfigurationValidatorTests
    {
        [Fact(DisplayName = "BootstrapConfigurationValidator can be created.")]
        [Trait("Category", "Unit")]
        public void CanCreateBootstrapConfigurationValidator()
        {
            // Act
            var exception = Record.Exception(() => new BootstrapConfigurationValidator());

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = "BootstrapConfigurationValidator success on valid params.")]
        [Trait("Category", "Unit")]
        public void CanSuccessValidate()
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = new[]
                 {
                     "test"
                 }.ToList()
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().Be(ValidateOptionsResult.Success);
        }

        [Fact(DisplayName = "BootstrapConfigurationValidator success on valid params with size.")]
        [Trait("Category", "Unit")]
        public void CanSuccessValidateSize()
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = new[]
                 {
                     "test"
                 }.ToList(),
                MessageMaxBytes = 1024
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().Be(ValidateOptionsResult.Success);
        }

        [Fact(DisplayName = "BootstrapConfigurationValidator fails on null BootstrapServers params.")]
        [Trait("Category", "Unit")]
        public void CanFailValidateNullBootstrapServers()
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = null!
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().NotBe(ValidateOptionsResult.Success);
            result.Failed.Should().BeTrue();
        }

        [Fact(DisplayName = "BootstrapConfigurationValidator fails on empty BootstrapServers params.")]
        [Trait("Category", "Unit")]
        public void CanFailValidateEmptyBootstrapServers()
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = new List<string>()
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().NotBe(ValidateOptionsResult.Success);
            result.Failed.Should().BeTrue();
        }

        [Fact(DisplayName = "BootstrapConfigurationValidator fails on empty string in BootstrapServers params.")]
        [Trait("Category", "Unit")]
        public void CanFailValidateEmptyStringInBootstrapServers()
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = new[]
                 {
                     "test",
                     string.Empty
                 }.ToList()
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().NotBe(ValidateOptionsResult.Success);
            result.Failed.Should().BeTrue();
        }

        [Fact(DisplayName = "BootstrapConfigurationValidator fails on null string in BootstrapServers params.")]
        [Trait("Category", "Unit")]
        public void CanFailValidateNullStringInBootstrapServers()
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = new[]
                 {
                     "test",
                     null!
                 }.ToList()
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().NotBe(ValidateOptionsResult.Success);
            result.Failed.Should().BeTrue();
        }

        [Fact(DisplayName = "BootstrapConfigurationValidator fails on string with whitespaces in BootstrapServers params.")]
        [Trait("Category", "Unit")]
        public void CanFailValidateWhitespaceStringInBootstrapServers()
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = new[]
                 {
                     "test",
                     "     "
                 }.ToList()
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().NotBe(ValidateOptionsResult.Success);
            result.Failed.Should().BeTrue();
        }

        [Theory(DisplayName = "BootstrapConfigurationValidator fails on bad message size.")]
        [Trait("Category", "Unit")]
        [InlineData(0)]
        [InlineData(-1)]
        public void CanFailValidateBadMessageSize(int size)
        {

            // Arrange
            var validator = new BootstrapConfigurationValidator();
            var optinos = new BootstrapConfiguration()
            {
                BootstrapServers = new[]
                 {
                     "test"
                 }.ToList(),
                MessageMaxBytes = size
            };

            // Act
            ValidateOptionsResult result = validator.Validate(string.Empty, optinos);

            // Assert
            result.Should().NotBe(ValidateOptionsResult.Success);
            result.Failed.Should().BeTrue();
        }
    }
}
