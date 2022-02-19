using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace Logic.Tests
{
    public class KafkaSendHandlerTests
    {
        [Fact(DisplayName = "KafkaSendHandler could be created.")]
        [Trait("Category", "Unit")]
        public void CouldBeCreated()
        {
            // Act
            var exception = Record.Exception(() => new KafkaSendHandler(Mock.Of<IKafkaSender>(),
                                                                        Mock.Of<IJsonValidator>(),
                                                                        Mock.Of<ILogger<KafkaSendHandler>>()));

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = "KafkaSendHandler can't be created without sender.")]
        [Trait("Category", "Unit")]
        public void CantBeCreatedWithNullSender()
        {
            // Act
            var exception = Record.Exception(() => new KafkaSendHandler(null!,
                                                                        Mock.Of<IJsonValidator>(),
                                                                        Mock.Of<ILogger<KafkaSendHandler>>()));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "KafkaSendHandler can't be created without validator.")]
        [Trait("Category", "Unit")]
        public void CantBeCreatedWithNullValidator()
        {
            // Act
            var exception = Record.Exception(() => new KafkaSendHandler(Mock.Of<IKafkaSender>(),
                                                                        null!,
                                                                        Mock.Of<ILogger<KafkaSendHandler>>()));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "KafkaSendHandler can't be created without logger.")]
        [Trait("Category", "Unit")]
        public void CantBeCreatedWithNullLogger()
        {
            // Act
            var exception = Record.Exception(() => new KafkaSendHandler(Mock.Of<IKafkaSender>(),
                                                                        Mock.Of<IJsonValidator>(),
                                                                        null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "KafkaSendHandler valid key types should contains correct values.")]
        [Trait("Category", "Unit")]
        public void ContainsValidKeyTypes()
        {
            // Arrange
            var sender = new KafkaSendHandler(Mock.Of<IKafkaSender>(),
                                              Mock.Of<IJsonValidator>(),
                                              Mock.Of<ILogger<KafkaSendHandler>>());
            // Act
            var keyTypes = sender.ValidKeyTypes;

            // Assert
            keyTypes.Should().NotBeEmpty().And.HaveCount(4);
            keyTypes.Should().Contain(Models.KeyType.JSON);
            keyTypes.Should().Contain(Models.KeyType.NotSet);
            keyTypes.Should().Contain(Models.KeyType.String);
            keyTypes.Should().Contain(Models.KeyType.Long);
        }
    }
}
