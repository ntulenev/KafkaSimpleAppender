﻿using System;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace Logic.Tests;

public class JsonValidatorTests
{
    [Fact(DisplayName = "JsonValidator could be created.")]
    [Trait("Category", "Unit")]
    public void CouldBeCreated()
    {
        // Act
        var exception = Record.Exception(() => new JsonValidator(Mock.Of<ILogger<JsonValidator>>()));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = "JsonValidator cant be created if logger is null.")]
    [Trait("Category", "Unit")]
    public void CantBeCreatedWithNullLogger()
    {
        // Act
        var exception = Record.Exception(() => new JsonValidator(null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "JsonValidator could be validated with correct string.")]
    [Trait("Category", "Unit")]
    public void CanValidateCorrectString()
    {
        // Arrange
        var json = "{ \"test\" : 123  }";
        var validator = new JsonValidator(Mock.Of<ILogger<JsonValidator>>());

        // Act
        var isValid = validator.IsValid(json);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact(DisplayName = "JsonValidator can't be validated with null string.")]
    [Trait("Category", "Unit")]
    public void CantValidateNullString()
    {
        // Arrange
        var validator = new JsonValidator(Mock.Of<ILogger<JsonValidator>>());

        // Act
        var exception = Record.Exception(() => _ = validator.IsValid(null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Theory(DisplayName = "JsonValidator could be validated with incorrect string.")]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("123")]
    public void CanValidateIncorrectString(string json)
    {
        // Arrange
        var validator = new JsonValidator(Mock.Of<ILogger<JsonValidator>>());

        // Act
        var isValid = validator.IsValid(json);

        // Assert
        isValid.Should().BeFalse();
    }
}
