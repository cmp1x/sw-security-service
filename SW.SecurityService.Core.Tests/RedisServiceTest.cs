namespace SW.SecurityService.Core.Tests
{
    using Moq;
    using StackExchange.Redis;
    using SW.SecurityService.Core.Services;
    using Xunit;

    public class RedisServiceTest
    {
        [Fact]
        public void When_Requested_Specific_Key_Should_Return_Correct_Value()
        {
            // Arrange
            var testToken = "TargetToken";
            var expectedValue = "Name";
            var inputToken = "TargetToken";

            var multiplexerMock = new Mock<IConnectionMultiplexer>();
            var dbMock = new Mock<IDatabase>();

            multiplexerMock
                .Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(dbMock.Object);

            dbMock
                .Setup(db => db.StringGet(
                    It.Is<RedisKey>(rk => rk == testToken),
                    CommandFlags.None))
                .Returns(expectedValue);

            var sut = new RedisService(multiplexerMock.Object);

            // Act
            var actualResult = sut.Get(inputToken);

            // Assert
            Assert.Equal(expectedValue, actualResult);
        }

        [Fact]
        public void When_Requested_Wrong_Key_Should_Return_Null_Value()
        {
            // Arrange
            var testToken = "TargetToken";
            var wrongTokenToken = "WrongToken";
            var expectedValue = "Name";

            var multiplexerMock = new Mock<IConnectionMultiplexer>();
            var dbMock = new Mock<IDatabase>();

            multiplexerMock
                .Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(dbMock.Object);

            dbMock
                .Setup(db => db.StringGet(
                    It.Is<RedisKey>(rk => rk == testToken),
                    CommandFlags.None))
                .Returns(expectedValue);

            var sut = new RedisService(multiplexerMock.Object);

            // Act
            var actualResult = sut.Get(wrongTokenToken);

            // Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public void When_Setting_KeyValuePair_Should_Call_Db_SrtringSet()
        {
            // Arrange
            var targetValue = "TargetValue";
            var targetKey = "TargetKey";

            var multiplexerMock = new Mock<IConnectionMultiplexer>();
            var dbMock = new Mock<IDatabase>();

            multiplexerMock
                .Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(dbMock.Object);

            var sut = new RedisService(multiplexerMock.Object);

            // Act
            var actualResult = sut.Set(targetKey, targetValue);

            // Assert
            dbMock
                .Verify(
                    db => db.StringSet(
                        It.Is<RedisKey>(k => k == targetKey),
                        It.Is<RedisValue>(v => v == targetValue),
                        null,
                        false,
                        When.Always,
                        CommandFlags.None),
                    Times.Once);
        }

        [Fact]
        public void When_Setting_KeyValuePair_With_Value_null_Should_Call_Db_SrtringSet_With_Value_Null()
        {
            // Arrange
            string targetValue = null;
            var targetKey = "TargetKey";

            var multiplexerMock = new Mock<IConnectionMultiplexer>();
            var dbMock = new Mock<IDatabase>();

            multiplexerMock
                .Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(dbMock.Object);

            var sut = new RedisService(multiplexerMock.Object);

            // Act
            var actualResult = sut.Set(targetKey, targetValue);

            // Assert
            dbMock
                .Verify(
                    db => db.StringSet(
                        It.Is<RedisKey>(k => k == targetKey),
                        It.Is<RedisValue>(v => v == targetValue),
                        null,
                        false,
                        When.Always,
                        CommandFlags.None),
                    Times.Once);
        }

        [Fact]
        public void When_Succsesful_Setting_KeyValuePair_Should_Return_True()
        {
            // Arrange
            var targetValue = "TargetValue";
            var targetKey = "TargetKey";
            var expectedResult = true;

            var multiplexerMock = new Mock<IConnectionMultiplexer>();
            var dbMock = new Mock<IDatabase>();

            multiplexerMock
                .Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(dbMock.Object);

            dbMock
                .Setup(db => db.StringSet(
                        It.Is<RedisKey>(k => k == targetKey),
                        It.Is<RedisValue>(v => v == targetValue),
                        null,
                        false,
                        When.Always,
                        CommandFlags.None))
                .Returns(expectedResult);

            var sut = new RedisService(multiplexerMock.Object);

            // Act
            var actualResult = sut.Set(targetKey, targetValue);

            // Assert
            Assert.True(actualResult);
        }
    }
}
