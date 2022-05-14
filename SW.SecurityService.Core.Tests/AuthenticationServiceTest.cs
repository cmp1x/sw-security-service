namespace SW.SecurityService.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Moq;
    using Xunit;
    using Dapper;
    using StackExchange.Redis;
    using SW.SecurityService.CredentialRepository.Repository;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.CredentialRepository.Models;

    public class AuthenticationServiceTest
    {
        [Fact]
        public void When_Post_Propper_Password_Return_True()
        {
            //Arrange
            var propperPassword = "22";
            var targetName = "Nat";
            var targetCredential = new CredentialsDb()
            {
                User = targetName,
                Password = propperPassword
            };

            var dbMock = new Mock<ICredentialRepository>();
            var sut = new AuthenticationService(dbMock.Object);

            dbMock
                .Setup(db => db.GetCredential(targetName))
                .Returns(targetCredential);

            //Act
            var actualResult = sut.IsProperPassword(targetName, propperPassword);

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void When_Post_Not_Propper_Password_Return_False()
        {
            //Arrange
            var propperPassword = "22";
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

            //Act
            var actualResult = sut.IsProperPassword(targetName, propperPassword);

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void When_Post_Null_Password_Return_False()
        {
            //Arrange
            var propperPassword = "22";
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

            //Act
            var actualResult = sut.IsProperPassword(targetName, propperPassword);

            //Assert
            Assert.False(actualResult);
        }
    }
}
