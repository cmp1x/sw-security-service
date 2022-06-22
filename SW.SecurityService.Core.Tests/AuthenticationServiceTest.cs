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
            var expectedAuthenticationAnswer = new AuthenticationAnswer()
            {
                userRedis = new UserRedis()
                {
                    UserId = userId,
                    UserName = targetUserName,
                    TokenCreated = tokenCreatedDate,
                    AuthorizationToken = generatedToken
                }
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
            var actualAuthenticationAnswer = sut.AuthenticateUser(targetUserName, correctPassword);

            // Assert
            expectedAuthenticationAnswer.Should().BeEquivalentTo(actualAuthenticationAnswer);
        }

        [Fact]
        public void When_There_Are_No_Credentials_Return_AuthenticationAnswer_nonExistenLogin()
        {
            //Arrange
            var newUserName = "Kat";
            var newPasword = "33";
            var expectedAuthenticationAnswer = new AuthenticationAnswer()
            {
                nonExistenLogin = true
            };

            var tokenServiceMock = new Mock<ITokenService>();
            var tokenProviderMock = new Mock<ITokenProvider>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            var credentialsRepositoryMock = new Mock<ICredentialRepository>();

            var sut = new AuthenticationService(
                tokenServiceMock.Object,
                tokenProviderMock.Object,
                dateTimeProviderMock.Object,
                credentialsRepositoryMock.Object);

            //Act
            var actualAuthenticationAnswer = sut.AuthenticateUser(newUserName, newPasword);

            //Assert
            expectedAuthenticationAnswer.Should().BeEquivalentTo(actualAuthenticationAnswer);
        }

        [Fact]
        public void When_There_Are_Wrong_Password_Return_AuthenticationAnswer_wrongPassword()
        {
            //Arrange
            var targetUserName = "Slava";
            var targetPassword = "11";
            var wrongPassword = "L>JHdfsx5&^B";
            var targetCredential = new CredentialsDb()
            {
                Password = targetPassword,
                UserName = targetUserName
            };

            var expectedAuthenticationAnswer = new AuthenticationAnswer()
            {
                wrongPassword = true
            };

            var tokenServiceMock = new Mock<ITokenService>();
            var tokenProviderMock = new Mock<ITokenProvider>();
            var dateProviderMock = new Mock<IDateTimeProvider>();
            var credRepositiryMock = new Mock<ICredentialRepository>();

            var sut = new AuthenticationService(
                tokenServiceMock.Object,
                tokenProviderMock.Object,
                dateProviderMock.Object,
                credRepositiryMock.Object);

            credRepositiryMock
                .Setup(crm => crm.GetCredential(targetUserName))
                .Returns(targetCredential);

            //Act
            var actualAuthenticationAnswer = sut.AuthenticateUser(targetUserName, wrongPassword);

            //Assert
            expectedAuthenticationAnswer.Should().BeEquivalentTo(actualAuthenticationAnswer);
        }
    }
}
