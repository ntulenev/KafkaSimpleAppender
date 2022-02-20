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

        [Fact(DisplayName = "KafkaSendHandler can't handle wrong key type.")]
        [Trait("Category", "Unit")]
        public async Task CantHandleWrongKeyType()
        {
            // Arrange
            var topicName = "test";
            var key = "testKey";
            var value = "testValue";
            var isJson = false;
            using var cts = new CancellationTokenSource();
            var sender = new KafkaSendHandler(Mock.Of<IKafkaSender>(),
                                              Mock.Of<IJsonValidator>(),
                                              Mock.Of<ILogger<KafkaSendHandler>>());
            // Act
            var exception = await Record.ExceptionAsync(async () =>
                                                        await sender.HandleAsync(
                                                        topicName,
                                                        (Models.KeyType)int.MinValue,
                                                        key,
                                                        value,
                                                        isJson,
                                                        cts.Token));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact(DisplayName = "KafkaSendHandler validates json payload.")]
        [Trait("Category", "Unit")]
        public async Task ValidatesJsonPayload()
        {
            // Arrange
            var topicName = "test";
            var key = "testKey";
            var value = "testValue";
            var type = Models.KeyType.String;
            var isJson = true;

            var jsonValidatorMock = new Mock<IJsonValidator>(MockBehavior.Strict);
            jsonValidatorMock.Setup(x => x.IsValid(value)).Returns(false);

            using var cts = new CancellationTokenSource();

            var sender = new KafkaSendHandler(Mock.Of<IKafkaSender>(),
                                              jsonValidatorMock.Object,
                                              Mock.Of<ILogger<KafkaSendHandler>>());
            // Act
            var exception = await Record.ExceptionAsync(async () =>
                                                        await sender.HandleAsync(
                                                        topicName,
                                                        type,
                                                        key,
                                                        value,
                                                        isJson,
                                                        cts.Token));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }

        [Fact(DisplayName = "KafkaSendHandler validates json key.")]
        [Trait("Category", "Unit")]
        public async Task ValidatesJsonKey()
        {
            // Arrange
            var topicName = "test";
            var key = "testKey";
            var value = "testValue";
            var type = Models.KeyType.JSON;
            var isJson = false;

            var jsonValidatorMock = new Mock<IJsonValidator>(MockBehavior.Strict);
            jsonValidatorMock.Setup(x => x.IsValid(key)).Returns(false);

            using var cts = new CancellationTokenSource();

            var sender = new KafkaSendHandler(Mock.Of<IKafkaSender>(),
                                              jsonValidatorMock.Object,
                                              Mock.Of<ILogger<KafkaSendHandler>>());
            // Act
            var exception = await Record.ExceptionAsync(async () =>
                                                        await sender.HandleAsync(
                                                        topicName,
                                                        type,
                                                        key,
                                                        value,
                                                        isJson,
                                                        cts.Token));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }
    }
}
