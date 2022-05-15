namespace SW.SecurityService.Core.Tests
{
    using Moq;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.CredentialRepository.Models;
    using SW.SecurityService.CredentialRepository.Repository;
    using Xunit;

    public class AuthenticationServiceTest
    {
        [Fact]
        public void When_Passed_Correct_Password_Return_True()
        {
            // Arrange
            var correctPassword = "22";
            var targetName = "Nat";
            var targetCredential = new CredentialsDb()
            {
                User = targetName,
                Password = correctPassword
            };

            var dbMock = new Mock<ICredentialRepository>();
            var sut = new AuthenticationService(dbMock.Object);

            dbMock
                .Setup(db => db.GetCredential(targetName))
                .Returns(targetCredential);

            // Act
            var actualResult = sut.IsValidCredentials(targetName, correctPassword);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void When_Passed_Not_Propper_Password_Return_False()
        {
            // Arrange
            var correctPassword = "22";
            var targetName = "Nat";
            var targetCredential = new CredentialsDb()
            {
                User = targetName,
                Password = "33"
            };

            var dbMock = new Mock<ICredentialRepository>();
            var sut = new AuthenticationService(dbMock.Object);

            dbMock
                .Setup(db => db.GetCredential(targetName))
                .Returns(targetCredential);

            // Act
            var actualResult = sut.IsValidCredentials(targetName, correctPassword);

            // Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void When_Passed_Null_Password_Return_False()
        {
            // Arrange
            var correctPassword = "22";
            var targetName = "Nat";
            var targetCredential = new CredentialsDb()
            {
                User = targetName,
                Password = string.Empty
            };

            var dbMock = new Mock<ICredentialRepository>();
            var sut = new AuthenticationService(dbMock.Object);

            dbMock
                .Setup(db => db.GetCredential(targetName))
                .Returns(targetCredential);

            // Act
            var actualResult = sut.IsValidCredentials(targetName, correctPassword);

            // Assert
            Assert.False(actualResult);
        }
    }
}
