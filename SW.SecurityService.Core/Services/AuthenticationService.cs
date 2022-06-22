namespace SW.SecurityService.Core.Services
{
    using System;
    using Newtonsoft.Json;
    using SW.SecurityService.Core.Enums;
    using SW.SecurityService.Core.Models;
    using SW.SecurityService.Core.Providers;
    using SW.SecurityService.CredentialRepository.Repository;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService tokenService;
        private readonly ITokenProvider tokenProvider;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ICredentialRepository credentialsRepository;

        public AuthenticationService(
            ITokenService tokenService,
            ITokenProvider tokenProvider,
            IDateTimeProvider dateTimeProvider,
            ICredentialRepository credentialsRepository)
        {
            this.tokenService = tokenService;
            this.tokenProvider = tokenProvider;
            this.dateTimeProvider = dateTimeProvider;
            this.credentialsRepository = credentialsRepository;
        }

        public AuthenticationAnswer AuthenticateUser(string userName, string password)
        {
            var actualCredentials = this.credentialsRepository
                .GetCredential(userName);

            var authenticationAnswer = new AuthenticationAnswer();
            
            if (actualCredentials is null)
            {
                authenticationAnswer.Status = AuthenticationAnswerStatus.UserNotExist;

                return authenticationAnswer;
            }

            if (password == actualCredentials.Password)
            {
                var token = this.tokenProvider.GetNewToken();
                var userRedis = new UserRedis()
                {
                    UserId = actualCredentials.UserId,
                    UserName = actualCredentials.UserName,
                    TokenCreated = this.dateTimeProvider.Now(),
                    AuthorizationToken = token
                };

                this.tokenService.Set(
                    token,
                    JsonConvert.SerializeObject(userRedis));
                authenticationAnswer.userRedis = userRedis;
                authenticationAnswer.Status = AuthenticationAnswerStatus.Authorized;
                
                return authenticationAnswer;
            }

            authenticationAnswer.Status = AuthenticationAnswerStatus.WrongPassword;
            return authenticationAnswer;
        }
    }
}
