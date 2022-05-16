namespace SW.SecurityService.Core.Tests
{
    using FluentAssertions;
    using Moq;
    using SW.SecurityService.Core.Models;
    using SW.SecurityService.Core.Providers;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.CredentialRepository.Models;
    using SW.SecurityService.CredentialRepository.Repository;
    using System;
    using Xunit;

    public class AuthenticationServiceTest
    {
        [Fact]
        public void When_Passed_Correct_Password_Return_UserRedis()
        {
            // Arrange
            var targetUserName = "Nat";
            var correctPassword = "22";
            var userId = "vgGis4e_";
            var targetCredential = new CredentialsDb()
            {
                UserId = userId,
                UserName = targetUserName,
                Password = correctPassword
            };

            var generatedToken = "123123";
            var tokenCreatedDate = new DateTime(2007, 9, 1);
            var expectedUserRedis = new UserRedis()
            {
                UserId = userId,
                UserName = targetUserName,
                TokenCreated = tokenCreatedDate,
                CurrentToken = generatedToken
            };

            var redisMock = new Mock<ITokenService>();
            var tokenProviderMock = new Mock<ITokenProvider>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            var credentialRepositoryMock = new Mock<ICredentialRepository>();
            
            var sut = new AuthenticationService(
                redisMock.Object, 
                tokenProviderMock.Object,
                dateTimeProviderMock.Object,
                credentialRepositoryMock.Object);

            credentialRepositoryMock
                .Setup(crm => crm.GetCredential(It.Is<string>(un => un == targetUserName)))
                .Returns(targetCredential);

            tokenProviderMock
                .Setup(tpm => tpm.GetNewToken())
                .Returns(generatedToken);

            dateTimeProviderMock
                .Setup(dtpm => dtpm.Now())
                .Returns(tokenCreatedDate);

            // Act
            var actualUserRedis = sut.AuthenticateUser(targetUserName, correctPassword);

            // Assert
            expectedUserRedis.Should().BeEquivalentTo(actualUserRedis);
        }

    }
}
