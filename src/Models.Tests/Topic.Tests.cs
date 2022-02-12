using Xunit;

using System;
using FluentAssertions;

namespace Models.Tests
{
    public class TopicTests
    {
        [Fact(DisplayName = "Topic name can't be null.")]
        [Trait("Category", "Unit")]
        public void CantCreateWithNullName()
        {

            // Act
            var exception = Record.Exception(() => new Topic(null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Topic name can't be empty.")]
        [Trait("Category", "Unit")]
        public void CantCreateWithEmptyName()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = Record.Exception(() => new Topic(null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Topic name can't have any whitespaces.")]
        [Trait("Category", "Unit")]
        public void CantCreateWithWhitespacesInName()
        {
            // Arrange
            var name = "topic name";

            // Act
            var exception = Record.Exception(() => new Topic(null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Topic name can't have long names.")]
        [Trait("Category", "Unit")]
        public void CantCreateLongTopicName()
        {
            // Arrange
            var name = new string('x', 250);

            // Act
            var exception = Record.Exception(() => new Topic(null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Topic name can't have bad symbols names.")]
        [Trait("Category", "Unit")]
        public void CantCreateBadTopicName()
        {
            // Arrange
            var name = "ы?:%";

            // Act
            var exception = Record.Exception(() => new Topic(null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }


        [Fact(DisplayName = "Topic with valid name can be created.")]
        [Trait("Category", "Unit")]
        public void CanCreateValidTopic()
        {
            // Arrange
            var name = "test";

            // Act
            var exception = Record.Exception(() => new Topic(name));

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = "Topic contains name.")]
        [Trait("Category", "Unit")]
        public void ContainsName()
        {
            // Arrange
            var name = "test";

            // Act
            var topic = new Topic(name);

            // Assert
            topic.Name.Should().Be(name);
        }
    }
}
