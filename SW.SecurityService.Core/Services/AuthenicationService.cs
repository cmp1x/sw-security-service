namespace SW.SecurityService.Core.Services
{
    using SW.SecurityService.CredentialRepository.Repository;

    public class AuthenicationService : IAuthenticationService
    {
        private readonly ICredentialRepository credentialsRepository;

        public AuthenicationService(ICredentialRepository credentialsRepository)
        {
            this.credentialsRepository = credentialsRepository;
        }

        public bool IsProperPassword(string user, string password)
        {
            var actualCredentials = this.credentialsRepository.GetCredential(user);

            if (password == actualCredentials.Password)
            {
                return true;
            }

            return false;
        }
    }
}
